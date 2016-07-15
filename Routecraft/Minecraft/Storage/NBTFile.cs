using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTFile
    {
        public string FilePath { get; private set; }
        public NBTTag Root;

        public NBTFile()
        {
            this.Root = new NBTCompoundTag();
        }

        public NBTFile(string path)
        {
            this.Root = new NBTCompoundTag();
            this.FilePath = path;

            if (!File.Exists(path))
            {
                return;
            }
            this.Open(path);
        }

        public void Open(string path)
        {
            this.FilePath = path;

            FileStream LevelFile = File.Open(this.FilePath, FileMode.Open);
            LevelFile.Seek(-4, SeekOrigin.End);
            byte[] LengthArray = new byte[4];
            LevelFile.Read(LengthArray, 0, 4);
            int Length = BitConverter.ToInt32(LengthArray, 0);

            LevelFile.Seek(0, SeekOrigin.Begin);
            GZipStream Decompressor = new GZipStream(LevelFile, CompressionMode.Decompress, false);
            byte[] Data = new byte[Length];
            int TotalBytesRead = 0;
            while (TotalBytesRead < Length)
            {
                int BytesRead = 0;
                for (int i = 0; i < 4; i++)
                {
                    BytesRead = Decompressor.Read(Data, 0, Length);
                    if (BytesRead > 0)
                    {
                        break;
                    }
                }
                if (BytesRead == 0)
                {
                    Debug.WriteLine("Failed to read enough decompressed bytes from " + this.FilePath + ".");
                    break;
                }
                TotalBytesRead += BytesRead;
            }
            Decompressor.Close();
            LevelFile.Close();

            NBTReader NBTReader = new NBTReader(Data);
            this.Root = NBTReader.ReadNamedTag();
        }

        public void Save()
        {
            NBTWriter NBTWriter = new NBTWriter();
            NBTWriter.WriteNamedTag(this.Root);

            byte[] Data = NBTWriter.Data;

            if (!File.Exists(this.FilePath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(this.FilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(this.FilePath));
                }
            }
            FileStream LevelFile = File.Open(this.FilePath, FileMode.Create);

            GZipStream Compressor = new GZipStream(LevelFile, CompressionMode.Compress, true);
            Compressor.Write(Data, 0, Data.Length);
            Compressor.Flush();
            Compressor.Close();

            LevelFile.Write(BitConverter.GetBytes(NBTWriter.DataLength), 0, 4);
            LevelFile.Close();
        }
    }
}
