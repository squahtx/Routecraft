using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTByteArrayTag : NBTTag
    {
        private List<byte> bytes = new List<byte>();

        public NBTByteArrayTag()
        {
            this.Type = NBTTagType.ByteArray;
        }

        public void AddRange(IEnumerable<byte> bytes)
        {
            this.bytes.AddRange(bytes);
        }

        public int ByteCount
        {
            get
            {
                return this.bytes.Count;
            }
        }

        public IEnumerable<byte> Bytes
        {
            get
            {
                return this.bytes;
            }
        }

        public void Clear()
        {
            this.bytes.Clear();
        }

        public void Set(IEnumerable<byte> bytes)
        {
            this.Clear();
            this.bytes.AddRange(bytes);
        }

        public override string ToString()
        {
            string ValueString = BitConverter.ToString(this.Bytes.ToArray());
            if (this.Name != null)
            {
                return this.Name.ToString() + " [" + this.ByteCount.ToString() + "]: " + ValueString;
            }
            return "byte[" + this.ByteCount.ToString() + "]: " + ValueString;
        }

        public byte this[int i]
        {
            get
            {
                return this.bytes[i];
            }
            set
            {
                this.bytes[i] = value;
            }
        }
    }
}
