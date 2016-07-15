using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft
{
    public class DataWriter
    {
        private List<byte> Buffer = new List<byte>();

        public DataWriter()
        {
        }

        public byte[] Bytes
        {
            get
            {
                return this.Buffer.ToArray();
            }
        }

        public void Clear()
        {
            this.Buffer.Clear();
        }

        public int Count
        {
            get
            {
                return this.Buffer.Count;
            }
        }

        public void WriteBytes(IEnumerable<byte> buffer)
        {
            this.Buffer.AddRange(buffer);
        }

        public void WriteBytes(byte[] buffer)
        {
            this.Buffer.AddRange(buffer);
        }

        public void WriteBytes(byte[] buffer, int size)
        {
            this.Buffer.AddRange(buffer.Take(size));
        }

        public void WriteUInt8(byte value)
        {
            this.Buffer.Add(value);
        }

        public void WriteInt8(sbyte value)
        {
            this.Buffer.Add((byte)value);
        }

        public void WriteUInt16(ushort value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteInt16(short value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteUInt24(uint value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Take(3).Reverse());
        }

        public void WriteUInt32(uint value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteInt32(int value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteInt64(long value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteUInt64(ulong value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteFloat(float value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteDouble(double value)
        {
            this.WriteBytes(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteString8(string value)
        {
            byte[] Bytes = Encoding.UTF8.GetBytes(value);
            this.WriteUInt16((ushort)Bytes.Length);
            this.WriteBytes(Bytes);
        }

        public void WriteString16(string value)
        {
            this.WriteUInt16((ushort)value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                this.WriteUInt16(value[i]);
            }
        }

        public void WriteUInt8Vector3(Vector3<byte> value)
        {
            this.WriteUInt8(value.x);
            this.WriteUInt8(value.y);
            this.WriteUInt8(value.z);
        }

        public void WriteInt8Vector3(Vector3<sbyte> value)
        {
            this.WriteInt8(value.x);
            this.WriteInt8(value.y);
            this.WriteInt8(value.z);
        }

        public void WriteInt16Vector3(Vector3<short> value)
        {
            this.WriteInt16(value.x);
            this.WriteInt16(value.y);
            this.WriteInt16(value.z);
        }

        public void WriteInt32Vector2(Vector2<int> value)
        {
            this.WriteInt32(value.x);
            this.WriteInt32(value.y);
        }

        public void WriteInt32Vector3(Vector3<int> value)
        {
            this.WriteInt32(value.x);
            this.WriteInt32(value.y);
            this.WriteInt32(value.z);
        }

        public void WriteFloatVector3(Vector3<float> value)
        {
            this.WriteFloat(value.x);
            this.WriteFloat(value.y);
            this.WriteFloat(value.z);
        }

        public void WriteDoubleVector3(Vector3<double> value)
        {
            this.WriteDouble(value.x);
            this.WriteDouble(value.y);
            this.WriteDouble(value.z);
        }
    }
}
