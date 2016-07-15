using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Storage.Regions;

namespace Routecraft.Minecraft
{
    public class Region
    {
        public int RefCount { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private World World;
        private Dimension Dimension;
        public string Path { get; private set; }

        public Vector2I RegionPos;

        private Chunk[,] Chunks = new Chunk[32, 32];

        public Region(World world, Dimension dimension, Vector2I regionPos)
        {
            this.World = world;
            this.Dimension = dimension;
            this.RegionPos = regionPos;

            this.Path = this.Dimension.Path + "\\region\\" + this.FileName;
            if (File.Exists(this.Path))
            {
                this.Open();
            }
        }

        public string FileName
        {
            get
            {
                return "r." + this.RegionPos.x + "." + this.RegionPos.y + ".mcr";
            }
        }

        #region Reference Counting
        public void AddRef()
        {
            this.RefCount++;
        }

        public void Release()
        {
            if (this.RefCount == 0)
            {
                return;
            }
            this.RefCount--;
        }
        #endregion

        public Vector2I ChunkMax
        {
            get
            {
                return this.ChunkMin + new Vector2I(31, 31);
            }
        }

        public Vector2I ChunkMin
        {
            get
            {
                return this.RegionPos * 32;
            }
        }

        public bool ChunkExists(Vector2I localPos)
        {
            return this.Chunks[localPos.y, localPos.x] != null;
        }

        public Chunk GetChunk(Vector2I localPos)
        {
            if (this.Chunks[localPos.y, localPos.x] == null)
            {
                this.Chunks[localPos.y, localPos.x] = new Chunk(this.World, this.Dimension, this, this.ChunkMin + localPos);
            }
            return this.Chunks[localPos.y, localPos.x];
        }

        public string ID
        {
            get
            {
                return this.Dimension.Type.ToString() + "/" + this.FileName;
            }
        }

        public bool IsChunkLoaded(Vector2I localPos)
        {
            if (this.Chunks[localPos.y, localPos.x] == null)
            {
                return false;
            }
            return this.Chunks[localPos.y, localPos.x].RefCount > 0;
        }

        #region Serialization
        public void Open()
        {
            RegionFileReader Reader = new RegionFileReader(this.Path);
            try
            {
                Reader.Read(this);
                Debug.WriteLine("Loaded region " + this.ID + ".");
            }
            catch
            {
                Debug.WriteLine("Region " + this.ID + " is corrupted.");
            }
        }

        public void Save()
        {
            RegionFileWriter Writer = new RegionFileWriter(this.Path);
            Writer.Write(this);
            Debug.WriteLine("Saved region " + this.ID + ".");
        }
        #endregion

        public Vector2I WorldToLocalChunk(Vector2I chunkPos)
        {
            return this.Dimension.WorldChunkPosToRegionChunkPos(chunkPos);
        }

        public override string ToString()
        {
            return "Region " + this.ChunkMin.ToString() + " - " + this.ChunkMax.ToString();
        }
    }
}
