using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTWriter
    {
        private DataWriter Writer = new DataWriter();

        public NBTWriter()
        {
        }

        public byte[] Data
        {
            get
            {
                return this.Writer.Bytes;
            }
        }

        public int DataLength
        {
            get
            {
                return this.Writer.Count;
            }
        }

        private void WriteString(string str)
        {
            if (str == null)
            {
                str = "";
            }
            byte[] Bytes = Encoding.UTF8.GetBytes(str);
            this.Writer.WriteInt16((short)Bytes.Length);
            this.Writer.WriteBytes(Bytes);
        }

        public void WriteNamedTag(NBTTag tag)
        {
            this.Writer.WriteUInt8((byte)tag.Type);
            this.WriteString(tag.Name);
            this.WriteTagContents(tag);
        }

        private void WriteTagContents(NBTTag tag)
        {
            switch (tag.Type)
            {
                case NBTTagType.Byte:
                    this.Writer.WriteUInt8(((NBTValueTag<byte>)tag).Value);
                    break;
                case NBTTagType.Short:
                    this.Writer.WriteInt16(((NBTValueTag<short>)tag).Value);
                    break;
                case NBTTagType.Int:
                    this.Writer.WriteInt32(((NBTValueTag<int>)tag).Value);
                    break;
                case NBTTagType.Long:
                    this.Writer.WriteInt64(((NBTValueTag<long>)tag).Value);
                    break;
                case NBTTagType.Float:
                    this.Writer.WriteFloat(((NBTValueTag<float>)tag).Value);
                    break;
                case NBTTagType.Double:
                    this.Writer.WriteDouble(((NBTValueTag<double>)tag).Value);
                    break;
                case NBTTagType.ByteArray:
                    NBTByteArrayTag ByteArrayTag = (NBTByteArrayTag)tag;
                    this.Writer.WriteInt32(ByteArrayTag.ByteCount);
                    this.Writer.WriteBytes(ByteArrayTag.Bytes);
                    break;
                case NBTTagType.String:
                    this.WriteString(((NBTValueTag<string>)tag).Value);
                    break;
                case NBTTagType.List:
                    NBTListTag ListTag = (NBTListTag)tag;
                    this.Writer.WriteUInt8((byte)ListTag.ChildrenType);
                    this.Writer.WriteInt32(ListTag.ChildCount);
                    foreach (NBTTag Child in ListTag.Children)
                    {
                        this.WriteTagContents(Child);
                    }
                    break;
                case NBTTagType.Compound:
                    NBTCompoundTag CompoundTag = (NBTCompoundTag)tag;
                    foreach (NBTTag Child in CompoundTag.Children)
                    {
                        this.WriteNamedTag(Child);
                    }
                    this.Writer.WriteUInt8((byte)NBTTagType.End);
                    break;
            }
        }
    }
}
