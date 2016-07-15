using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft
{
    public class DataReader
    {
        private List<byte> Buffer = null;
        private byte[] Minibuffer = new byte[8];
        private int Size = 0;

        public int Position = 0;

        public DataReader()
        {
        }

        public DataReader(IEnumerable<byte> buffer)
        {
            this.Initialize(buffer.ToList());
        }

        public void Initialize(List<byte> buffer)
        {
            this.Buffer = buffer;
            this.Size = buffer.Count;
            this.Position = 0;
        }

        public void Initialize(List<byte> buffer, int size)
        {
            this.Buffer = buffer;
            this.Size = size;
            this.Position = 0;
        }

        public int BytesRemaining
        {
            get
            {
                return this.Size - this.Position;
            }
        }

        public bool CanReadString16
        {
            get
            {
                if (this.BytesRemaining < 2) { return false; }
                ushort Length = this.ReadUInt16();
                this.Position -= 2;
                if (this.BytesRemaining < 2 + Length * 2) { return false; }
                return true;
            }
        }

        public int Count
        {
            get
            {
                return this.Size;
            }
        }

        public byte[] ReadBytes(uint count)
        {
            return this.ReadBytes((int)count);
        }

        public byte[] ReadBytes(int count)
        {
            byte[] Value = new byte[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = this.Buffer[this.Position + i];
            }
            this.Position += count;
            return Value;
        }

        public byte ReadUInt8()
        {
            byte Value = this.Buffer[this.Position];
            this.Position++;
            return Value;
        }

        public sbyte ReadInt8()
        {
            sbyte Value = (sbyte)this.Buffer[this.Position];
            this.Position++;
            return Value;
        }

        public ushort ReadUInt16()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 1];
            this.Minibuffer[1] = this.Buffer[this.Position];
            ushort Value = BitConverter.ToUInt16(this.Minibuffer, 0);
            this.Position += 2;
            return Value;
        }

        public short ReadInt16()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 1];
            this.Minibuffer[1] = this.Buffer[this.Position];
            short Value = BitConverter.ToInt16(this.Minibuffer, 0);
            this.Position += 2;
            return Value;
        }

        public uint ReadUInt24()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 2];
            this.Minibuffer[1] = this.Buffer[this.Position + 1];
            this.Minibuffer[2] = this.Buffer[this.Position];
            this.Minibuffer[3] = 0;
            uint Value = BitConverter.ToUInt32(this.Minibuffer, 0);
            this.Position += 3;
            return Value;
        }

        public uint ReadUInt32()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 3];
            this.Minibuffer[1] = this.Buffer[this.Position + 2];
            this.Minibuffer[2] = this.Buffer[this.Position + 1];
            this.Minibuffer[3] = this.Buffer[this.Position];
            uint Value = BitConverter.ToUInt32(this.Minibuffer, 0);
            this.Position += 4;
            return Value;
        }

        public int ReadInt32()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 3];
            this.Minibuffer[1] = this.Buffer[this.Position + 2];
            this.Minibuffer[2] = this.Buffer[this.Position + 1];
            this.Minibuffer[3] = this.Buffer[this.Position];
            int Value = BitConverter.ToInt32(this.Minibuffer, 0);
            this.Position += 4;
            return Value;
        }

        public ulong ReadUInt64()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 7];
            this.Minibuffer[1] = this.Buffer[this.Position + 6];
            this.Minibuffer[2] = this.Buffer[this.Position + 5];
            this.Minibuffer[3] = this.Buffer[this.Position + 4];
            this.Minibuffer[4] = this.Buffer[this.Position + 3];
            this.Minibuffer[5] = this.Buffer[this.Position + 2];
            this.Minibuffer[6] = this.Buffer[this.Position + 1];
            this.Minibuffer[7] = this.Buffer[this.Position];
            ulong Value = BitConverter.ToUInt64(this.Minibuffer, 0);
            this.Position += 8;
            return Value;
        }

        public long ReadInt64()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 7];
            this.Minibuffer[1] = this.Buffer[this.Position + 6];
            this.Minibuffer[2] = this.Buffer[this.Position + 5];
            this.Minibuffer[3] = this.Buffer[this.Position + 4];
            this.Minibuffer[4] = this.Buffer[this.Position + 3];
            this.Minibuffer[5] = this.Buffer[this.Position + 2];
            this.Minibuffer[6] = this.Buffer[this.Position + 1];
            this.Minibuffer[7] = this.Buffer[this.Position];
            long Value = BitConverter.ToInt64(this.Minibuffer, 0);
            this.Position += 8;
            return Value;
        }

        public float ReadFloat()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 3];
            this.Minibuffer[1] = this.Buffer[this.Position + 2];
            this.Minibuffer[2] = this.Buffer[this.Position + 1];
            this.Minibuffer[3] = this.Buffer[this.Position];
            float Value = BitConverter.ToSingle(this.Minibuffer, 0);
            this.Position += 4;
            return Value;
        }

        public double ReadDouble()
        {
            this.Minibuffer[0] = this.Buffer[this.Position + 7];
            this.Minibuffer[1] = this.Buffer[this.Position + 6];
            this.Minibuffer[2] = this.Buffer[this.Position + 5];
            this.Minibuffer[3] = this.Buffer[this.Position + 4];
            this.Minibuffer[4] = this.Buffer[this.Position + 3];
            this.Minibuffer[5] = this.Buffer[this.Position + 2];
            this.Minibuffer[6] = this.Buffer[this.Position + 1];
            this.Minibuffer[7] = this.Buffer[this.Position];
            double Value = BitConverter.ToDouble(this.Minibuffer, 0);
            this.Position += 8;
            return Value;
        }

        public string ReadString8()
        {
            ushort Length = this.ReadUInt16();
            string Value = "";
            for (int i = 0; i < Length; i++)
            {
                Value += (char)this.Buffer[this.Position + i];
            }
            this.Position += Length;

            return Value;
        }

        public string ReadString16()
        {
            ushort Length = this.ReadUInt16();
            string Value = "";
            for (int i = 0; i < Length; i++)
            {
                Value += (char)this.ReadUInt16();
            }

            return Value;
        }

        public Vector3<byte> ReadUInt8Vector3()
        {
            Vector3<byte> Value = new Vector3<byte>();
            Value.x = this.ReadUInt8();
            Value.y = this.ReadUInt8();
            Value.z = this.ReadUInt8();
            return Value;
        }

        public Vector3<sbyte> ReadInt8Vector3()
        {
            Vector3<sbyte> Value = new Vector3<sbyte>();
            Value.x = this.ReadInt8();
            Value.y = this.ReadInt8();
            Value.z = this.ReadInt8();
            return Value;
        }

        public Vector3<short> ReadInt16Vector3()
        {
            Vector3<short> Value = new Vector3<short>();
            Value.x = this.ReadInt16();
            Value.y = this.ReadInt16();
            Value.z = this.ReadInt16();
            return Value;
        }

        public Vector2<int> ReadInt32Vector2()
        {
            Vector2<int> Value = new Vector2<int>();
            Value.x = this.ReadInt32();
            Value.y = this.ReadInt32();
            return Value;
        }

        public Vector3<int> ReadInt32Vector3()
        {
            Vector3<int> Value = new Vector3<int>();
            Value.x = this.ReadInt32();
            Value.y = this.ReadInt32();
            Value.z = this.ReadInt32();
            return Value;
        }

        public Vector3<float> ReadFloatVector3()
        {
            Vector3<float> Value = new Vector3<float>();
            Value.x = this.ReadFloat();
            Value.y = this.ReadFloat();
            Value.z = this.ReadFloat();
            return Value;
        }

        public Vector3<double> ReadDoubleVector3()
        {
            Vector3<double> Value = new Vector3<double>();
            Value.x = this.ReadDouble();
            Value.y = this.ReadDouble();
            Value.z = this.ReadDouble();
            return Value;
        }
    }
}
