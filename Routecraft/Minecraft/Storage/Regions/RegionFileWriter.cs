using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage.Regions
{
    public class RegionFileWriter
    {
        public string FilePath { get; private set; }

        public RegionFileWriter(string path)
        {
            this.FilePath = path;
        }

        public void Write(Region region)
        {
            List<Vector2I> ChunksToSave = new List<Vector2I>();
            int Offset = 2;

            if (!File.Exists(this.FilePath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(this.FilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(this.FilePath));
                }
            }
            FileStream RegionFile = File.Open(this.FilePath, FileMode.Create);
            DataWriter Writer = new DataWriter();

            // Chunk locations
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (region.ChunkExists(new Vector2I(x, y)))
                    {
                        Chunk Chunk = region.GetChunk(new Vector2I(x, y));
                        Chunk.Compress();
                        ChunksToSave.Add(new Vector2I(x, y));
                        Writer.WriteUInt24((uint)Offset);
                        Writer.WriteUInt8((byte)((Chunk.CompressedData.Length + 5 + 4095) / 4096));

                        Offset += (Chunk.CompressedData.Length + 5 + 4095) / 4096;
                    }
                    else
                    {
                        Writer.WriteUInt32(0);
                    }
                }
            }
            RegionFile.Write(Writer.Bytes, 0, Writer.Count);
            Writer.Clear();

            // Chunk timestamps
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (region.ChunkExists(new Vector2I(x, y)))
                    {
                        Writer.WriteUInt32(region.GetChunk(new Vector2I(x, y)).Timestamp);
                    }
                    else
                    {
                        Writer.WriteUInt32(0);
                    }
                }
            }
            RegionFile.Write(Writer.Bytes, 0, Writer.Count);
            Writer.Clear();

            // Chunk data
            foreach (Vector2I chunkPos in ChunksToSave)
            {
                Chunk Chunk = region.GetChunk(chunkPos);
                int AllocatedSize = (Chunk.CompressedData.Length + 5 + 4095) / 4096 * 4096;

                Writer.WriteInt32(Chunk.CompressedData.Length + 1);
                Writer.WriteUInt8((byte)Chunk.CompressionMethod);
                RegionFile.Write(Writer.Bytes, 0, Writer.Count);
                RegionFile.Write(Chunk.CompressedData, 0, Chunk.CompressedData.Length);

                int RemainingAllocation = AllocatedSize - Writer.Count - Chunk.CompressedData.Length;
                for (int i = 0; i < RemainingAllocation; i++)
                {
                    RegionFile.WriteByte(0);
                }

                Writer.Clear();
            }

            RegionFile.Close();
        }
    }
}
