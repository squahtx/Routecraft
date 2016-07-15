using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage.Regions
{
    public class RegionFileReader
    {
        public string Path { get; private set; }

        public RegionFileReader(string path)
        {
            this.Path = path;
        }

        public void Read(Region region)
        {
            FileStream RegionFile = File.Open(this.Path, FileMode.Open);

            byte[] Data = new byte[RegionFile.Length];
            RegionFile.Read(Data, 0, (int)RegionFile.Length);
            RegionFile.Close();

            DataReader Reader = new DataReader(Data);
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    int Offset = (int)Reader.ReadUInt24();
                    byte SectorCount = Reader.ReadUInt8();

                    int Position = Reader.Position;

                    if (Offset != 0)
                    {
                        uint Timestamp = Reader.ReadUInt32();
                        this.ReadChunk(region, Reader, new Vector2I(x, y), Offset, SectorCount, Timestamp);
                        Reader.Position = Position;
                    }
                }
            }
        }

        private void ReadChunk(Region region, DataReader reader, Vector2I localPos, int offset, byte sectorCount, uint timestamp)
        {
            Chunk Chunk = region.GetChunk(localPos);
            Chunk.Timestamp = timestamp;

            reader.Position = 4096 * offset;

            uint CompressedLength = reader.ReadUInt32() - 1;
            byte CompressionMethod = reader.ReadUInt8();
            byte[] CompressedData = reader.ReadBytes(CompressedLength);

            Chunk.CompressionMethod = CompressionMethod;
            Chunk.CompressedData = CompressedData;
            Chunk.NeedsDecompression = true;
        }
    }
}
