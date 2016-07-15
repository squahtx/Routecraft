using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class MapChunkPacket : IPacket
    {
        public int X;
        public short Y;
        public int Z;
        public Vector3<byte> Size = new Vector3<byte>();
        public uint CompressedDataSize;
        public byte[] CompressedData;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 17) { return false; }
            this.X = reader.ReadInt32();
            this.Y = reader.ReadInt16();
            this.Z = reader.ReadInt32();
            this.Size = reader.ReadUInt8Vector3();
            this.CompressedDataSize = reader.ReadUInt32();
            if (reader.BytesRemaining < this.CompressedDataSize) { return false; }
            this.CompressedData = reader.ReadBytes(this.CompressedDataSize);

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32(this.X);
            writer.WriteInt16(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteUInt8Vector3(this.Size);
            writer.WriteUInt32(this.CompressedDataSize);
            writer.WriteBytes(this.CompressedData, (int)this.CompressedDataSize);
        }

        public uint DecompressedDataLength
        {
            get
            {
                return (this.Volume * 5) / 2;
            }
        }

        /// <summary>
        /// Stores the decompressed data in the given array
        /// </summary>
        /// <param name="data"></param>
        public void GetDecompressedData(byte[] data)
        {
            uint ExpectedSize = (this.Volume * 5) / 2;

            if (data.Length < ExpectedSize)
            {
                throw new ArgumentException("data should be at least Volume * 5 / 2 elements long.", "data");
            }

            Inflater Inflater = new Inflater();
            Inflater.SetInput(this.CompressedData);
            uint DecompressedSize = (uint)Inflater.Inflate(data);
            if (DecompressedSize != ExpectedSize)
            {
                Debug.WriteLine("MapChunkPacket: Expected " + ExpectedSize.ToString() + " bytes, but got " + DecompressedSize.ToString() + ".");
            }
        }

        public void SetDecompressedData(byte[] data)
        {
            this.SetDecompressedData(data, data.Length);
        }

        public void SetDecompressedData(byte[] data, int size)
        {
            Deflater Deflater = new Deflater(4);
            Deflater.SetInput(data, 0, size);
            Deflater.Finish();

            int RecompressedDataSize = 0;
            byte[] RecompressedData = new byte[(this.Volume * 5) / 2];
            while (!Deflater.IsFinished)
            {
                int DeltaLength = Deflater.Deflate(RecompressedData, RecompressedDataSize, RecompressedData.Length - RecompressedDataSize);
                RecompressedDataSize += DeltaLength;
            }
            this.CompressedDataSize = (uint)RecompressedDataSize;
            this.CompressedData = RecompressedData;
        }

        public uint Volume
        {
            get
            {
                return (uint)((this.Size.x + 1) * (this.Size.y + 1) * (this.Size.z + 1));
            }
        }
    }
}
