using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft
{
    public class ChunkBufferPool
    {
        public Vector3I ChunkSize { get; private set; }

        private List<byte[]> FullChunks = new List<byte[]>(1024);
        private List<byte[]> HalfChunks = new List<byte[]>(4096);

        public ChunkBufferPool(Vector3I size)
        {
            this.ChunkSize = size;
        }

        public byte[] AllocateFullChunk()
        {
            if (this.FullChunks.Count == 0)
            {
                return new byte[this.ChunkSize.Volume];
            }

            byte[] Buffer = this.FullChunks[this.FullChunks.Count - 1];
            this.FullChunks.RemoveAt(this.FullChunks.Count - 1);

            // Clear the buffer
            for (int i = 0; i < Buffer.Length; i++)
            {
                Buffer[i] = 0;
            }

            return Buffer;
        }

        public byte[] AllocateHalfChunk()
        {
            if (this.HalfChunks.Count == 0)
            {
                return new byte[this.ChunkSize.Volume / 2];
            }

            byte[] Buffer = this.HalfChunks[this.HalfChunks.Count - 1];
            this.HalfChunks.RemoveAt(this.HalfChunks.Count - 1);

            // Clear the buffer
            for (int i = 0; i < Buffer.Length; i++)
            {
                Buffer[i] = 0;
            }

            return Buffer;
        }

        public void FreeFullChunk(byte[] buffer)
        {
            if (buffer.Length != this.ChunkSize.Volume)
            {
                throw new ArgumentException("buffer should be " + this.ChunkSize.Volume.ToString() + " elements long.", "buffer");
            }
            this.FullChunks.Add(buffer);
        }

        public void FreeHalfChunk(byte[] buffer)
        {
            if (buffer.Length != this.ChunkSize.Volume)
            {
                throw new ArgumentException("buffer should be " + (this.ChunkSize.Volume / 2).ToString() + " elements long.", "buffer");
            }
            this.HalfChunks.Add(buffer);
        }
    }
}
