using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTReader
    {
        private byte[] Data;

        private DataReader Reader;

        public NBTReader(byte[] Data)
        {
            this.Data = Data;
            this.Reader = new DataReader(Data);
        }

        private string ReadString()
        {
            short Length = this.Reader.ReadInt16();
            this.Reader.Position += Length;
            return Encoding.UTF8.GetString(this.Data, this.Reader.Position - Length, Length);
        }

        public NBTTag ReadNamedTag()
        {
            NBTTagType Type = (NBTTagType)this.Reader.ReadUInt8();
            if (Type == NBTTagType.End)
            {
                return null;
            }
            string Name = this.ReadString();
            return this.ReadTagType(Type, Name);
        }

        private NBTByteTag ReadByteTag(string Name)
        {
            NBTByteTag Tag = new NBTByteTag();
            Tag.Name = Name;
            Tag.Value = this.Reader.ReadUInt8();
            return Tag;
        }

        private NBTByteArrayTag ReadByteArrayTag(string Name)
        {
            NBTByteArrayTag Tag = new NBTByteArrayTag();
            Tag.Name = Name;
            int ByteCount = this.Reader.ReadInt32();
            Tag.AddRange(this.Reader.ReadBytes(ByteCount));
            return Tag;
        }

        private NBTCompoundTag ReadCompoundTag(string Name)
        {
            NBTCompoundTag Tag = new NBTCompoundTag();
            Tag.Name = Name;

            NBTTag Child = this.ReadNamedTag();
            while (Child != null)
            {
                Tag.AddChild(Child);
                Child = this.ReadNamedTag();
            }
            return Tag;
        }

        private NBTDoubleTag ReadDoubleTag(string Name)
        {
            NBTDoubleTag Tag = new NBTDoubleTag();
            Tag.Name = Name;
            Tag.Value = this.Reader.ReadDouble();
            return Tag;
        }

        private NBTFloatTag ReadFloatTag(string Name)
        {
            NBTFloatTag Tag = new NBTFloatTag();
            Tag.Name = Name;
            Tag.Value = this.Reader.ReadFloat();
            return Tag;
        }

        private NBTIntTag ReadIntTag(string Name)
        {
            NBTIntTag Tag = new NBTIntTag();
            Tag.Name = Name;
            Tag.Value = this.Reader.ReadInt32();
            return Tag;
        }

        private NBTListTag ReadListTag(string Name)
        {
            NBTListTag Tag = new NBTListTag();
            Tag.Name = Name;

            Tag.ChildrenType = (NBTTagType)this.Reader.ReadUInt8();
            int Length = this.Reader.ReadInt32();

            for (int i = 0; i < Length; i++)
            {
                Tag.AddChild(this.ReadTagType(Tag.ChildrenType, null));
            }
            return Tag;
        }

        private NBTLongTag ReadLongTag(string Name)
        {
            NBTLongTag Tag = new NBTLongTag();
            Tag.Name = Name;
            Tag.Value = this.Reader.ReadInt64();
            return Tag;
        }

        private NBTShortTag ReadShortTag(string Name)
        {
            NBTShortTag Tag = new NBTShortTag();
            Tag.Name = Name;
            Tag.Value = this.Reader.ReadInt16();
            return Tag;
        }

        private NBTStringTag ReadStringTag(string Name)
        {
            NBTStringTag Tag = new NBTStringTag();
            Tag.Name = Name;
            Tag.Value = this.ReadString();
            return Tag;
        }

        private NBTTag ReadTagType(NBTTagType Type, string Name)
        {
            switch (Type)
            {
                case NBTTagType.Byte:
                    return this.ReadByteTag(Name);
                case NBTTagType.Short:
                    return this.ReadShortTag(Name);
                case NBTTagType.Int:
                    return this.ReadIntTag(Name);
                case NBTTagType.Long:
                    return this.ReadLongTag(Name);
                case NBTTagType.Float:
                    return this.ReadFloatTag(Name);
                case NBTTagType.Double:
                    return this.ReadDoubleTag(Name);
                case NBTTagType.ByteArray:
                    return this.ReadByteArrayTag(Name);
                case NBTTagType.String:
                    return this.ReadStringTag(Name);
                case NBTTagType.List:
                    return this.ReadListTag(Name);
                case NBTTagType.Compound:
                    return this.ReadCompoundTag(Name);
            }
            return null;
        }

        public NBTTag ReadUnnamedTag()
        {
            NBTTagType Type = (NBTTagType)this.Reader.ReadUInt8();
            if (Type == NBTTagType.End)
            {
                return null;
            }
            return this.ReadTagType(Type, null);
        }
    }
}
