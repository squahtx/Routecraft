using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;
using Routecraft.Minecraft.Storage;

namespace Routecraft.Minecraft
{
    public class Chunk
    {
        public int RefCount { get; private set; }

        private World World;
        private Dimension Dimension;
        private Region Region;

        public Vector2I WorldChunkPos { get; private set; }

        public uint Timestamp;

        public int CompressionMethod = 0;
        public byte[] CompressedData = null;
        public bool NeedsDecompression = false;
        public bool NeedsCompression = false;

        public Vector3I ChunkSize = new Vector3I(16, 16, 128);
        private bool MemoryAllocated = false;
        public byte[, ,] BlockTypes;
        public byte[, ,] BlockSubTypes;
        public byte[, ,] SkyLight;
        public byte[, ,] BlockLight;

        public Chunk(World world, Dimension dimension, Region region, Vector2I chunkPos)
        {
            this.World = world;
            this.Dimension = dimension;
            this.Region = region;

            this.WorldChunkPos = chunkPos;
        }

        #region Reference Counting
        public void AddRef()
        {
            this.RefCount++;
            this.Region.AddRef();
        }

        public void Release()
        {
            if (this.RefCount == 0)
            {
                return;
            }
            this.RefCount--;
            this.Region.Release();

            if (this.RefCount == 0)
            {
                this.Compress();
                this.DeallocateMemory();
            }
        }
        #endregion

        #region Serialization
        private static byte[] DecompressionBuffer = new byte[4096];
        public void Decompress()
        {
            if (!this.NeedsDecompression) { return; }
            if (!this.MemoryAllocated) { this.AllocateMemory(); }
            
            Inflater Inflater = new Inflater();
            Inflater.SetInput(this.CompressedData);
            List<byte> DecompressedData = new List<byte>();

            lock (Chunk.DecompressionBuffer)
            {
                while (!Inflater.IsNeedingInput)
                {
                    int BytesFilled = Inflater.Inflate(Chunk.DecompressionBuffer, 0, Chunk.DecompressionBuffer.Length);
                    DecompressedData.AddRange(Chunk.DecompressionBuffer.Take(BytesFilled));
                }
            }

            NBTReader NBTReader = new NBTReader(DecompressedData.ToArray());
            NBTCompoundTag Level = (NBTCompoundTag)((NBTCompoundTag)NBTReader.ReadNamedTag()).Children.First();

            if (Level.GetValue<int>("xPos") != this.WorldChunkPos.x ||
                Level.GetValue<int>("zPos") != this.WorldChunkPos.y)
            {
                throw new InvalidDataException();
            }

            NBTByteArrayTag Blocks = (NBTByteArrayTag)Level.GetChild("Blocks");
            NBTByteArrayTag BlockData = (NBTByteArrayTag)Level.GetChild("Data");
            NBTByteArrayTag SkyLighting = (NBTByteArrayTag)Level.GetChild("SkyLight");
            NBTByteArrayTag BlockLighting = (NBTByteArrayTag)Level.GetChild("BlockLight");
            for (int z = 0; z < this.ChunkSize.z; z++)
            {
                for (int y = 0; y < this.ChunkSize.y; y++)
                {
                    for (int x = 0; x < this.ChunkSize.x; x++)
                    {
                        int Index = z + y * this.ChunkSize.z + x * this.ChunkSize.z * this.ChunkSize.y;
                        this.BlockTypes[z, y, x] = Blocks[Index];

                        bool UseMSB = Index % 2 == 1;
                        Index /= 2;

                        byte SubType = BlockData[Index];
                        byte SkyLight = SkyLighting[Index];
                        byte BlockLight = BlockLighting[Index];

                        if (UseMSB)
                        {
                            SubType >>= 4;
                            SkyLight >>= 4;
                            BlockLight >>= 4;
                        }
                        else
                        {
                            SubType &= 0x0F;
                            SkyLight &= 0x0F;
                            BlockLight &= 0x0F;
                        }

                        this.BlockSubTypes[z, y, x] = SubType;
                        this.SkyLight[z, y, x] = SkyLight;
                        this.BlockLight[z, y, x] = BlockLight;

                    }
                }
            }

            this.NeedsCompression = false;
            this.NeedsDecompression = false;
        }

        private static byte[] CompressionBuffer = new byte[4096];
        private static byte[] NBTBlockData = new byte[16 * 16 * 128];
        private static byte[] NBTBlockNibbleData = new byte[16 * 16 * 128 / 2];
        private static byte[] NBTHeightMap = new byte[16 * 16];
        public void Compress()
        {
            if (!this.NeedsCompression)
            {
                return;
            }

            NBTWriter Writer = new NBTWriter();
            NBTCompoundTag Root = new NBTCompoundTag();
            {
                NBTCompoundTag Level = Root.AddChild<NBTCompoundTag>("Level");
                {
                    lock (Chunk.NBTBlockData)
                    {
                        this.GetBlocks(Chunk.NBTBlockData);
                        Level.AddChild<NBTByteArrayTag>("Blocks").Set(Chunk.NBTBlockData);
                        this.GetBlockData(Chunk.NBTBlockNibbleData);
                        Level.AddChild<NBTByteArrayTag>("Data").Set(Chunk.NBTBlockNibbleData);
                        this.GetBlockLighting(Chunk.NBTBlockNibbleData);
                        Level.AddChild<NBTByteArrayTag>("BlockLight").Set(Chunk.NBTBlockNibbleData);
                        this.GetSkyLighting(Chunk.NBTBlockNibbleData);
                        Level.AddChild<NBTByteArrayTag>("SkyLight").Set(Chunk.NBTBlockNibbleData);
                        this.ComputeHeightMap(Chunk.NBTHeightMap);
                        Level.AddChild<NBTByteArrayTag>("HeightMap").Set(Chunk.NBTHeightMap);
                    }
                    NBTListTag Entities = Level.AddChild<NBTListTag>("Entities");
                    NBTListTag TileEntities = Level.AddChild<NBTListTag>("TileEntities");
                    NBTListTag TileTicks = Level.AddChild<NBTListTag>("TileTicks");
                    Level.AddChild<NBTLongTag>("LastUpdate").Value = 0;
                    Level.AddChild<NBTIntTag>("xPos").Value = this.WorldChunkPos.x;
                    Level.AddChild<NBTIntTag>("zPos").Value = this.WorldChunkPos.y;
                    Level.AddChild<NBTByteTag>("TerrainPopulated").Value = 1;
                }
            }

            Writer.WriteNamedTag(Root);

            Deflater Deflater = new Deflater(4);
            Deflater.SetInput(Writer.Data, 0, Writer.DataLength);
            Deflater.Finish();

            List<byte> CompressedData = new List<byte>();
            lock (Chunk.CompressionBuffer)
            {
                while (!Deflater.IsFinished)
                {
                    int BytesFilled = Deflater.Deflate(Chunk.CompressionBuffer, 0, Chunk.CompressionBuffer.Length);
                    CompressedData.AddRange(Chunk.CompressionBuffer.Take(BytesFilled));
                }
            }
            this.CompressedData = CompressedData.ToArray();

            this.CompressionMethod = 2;

            this.NeedsCompression = false;
            this.NeedsDecompression = false;
        }
        #endregion

        public Vector2I RegionChunkPos
        {
            get
            {
                return this.WorldChunkPos & 0x1F;
            }
        }

        public static int AllocatedChunks = 0;
        public void AllocateMemory()
        {
            if (this.MemoryAllocated) { return; }

            this.MemoryAllocated = true;
            Chunk.AllocatedChunks++;

            this.BlockTypes = new byte[128, 16, 16];
            this.BlockSubTypes = new byte[128, 16, 16];
            this.SkyLight = new byte[128, 16, 16];
            this.BlockLight = new byte[128, 16, 16];
        }

        public void DeallocateMemory()
        {
            if (!this.MemoryAllocated) { return; }
            if (this.NeedsCompression) { this.Compress(); }

            this.MemoryAllocated = false;
            Chunk.AllocatedChunks--;

            this.BlockTypes = null;
            this.BlockSubTypes = null;
            this.SkyLight = null;
            this.BlockLight = null;
        }

        #region Data Manipulation
        /// <summary>
        /// Computes and stores the chunk's heightmap in Minecraft format.
        /// </summary>
        /// <param name="heightMap">Array in which to store the heightmap.</param>
        public void ComputeHeightMap(byte[] heightMap)
        {
            if (heightMap.Length != this.ChunkSize.x * this.ChunkSize.y)
            {
                throw new ArgumentException("heightMap should be ChunkSize.x * ChunkSize.y elements long.", "heightMap");
            }

            for (int y = 0; y < this.ChunkSize.y; y++)
            {
                for (int x = 0; x < this.ChunkSize.x; x++)
                {
                    int z = this.ChunkSize.z - 1;
                    while (z >= 0)
                    {
                        if (this.BlockTypes[z, y, x] != (byte)ItemType.Air)
                        {
                            break;
                        }
                        z--;
                    }
                    z++;
                    if (z < 0) { z = 0; }
                    if (z > 255) { z = 255; }
                    heightMap[x + y * this.ChunkSize.x] = (byte)z;
                }
            }
        }

        /// <summary>
        /// Stores the blocks array in Minecraft format.
        /// </summary>
        /// <param name="blocks">Array in which to store the block data.
        /// Each block requires 1 byte.</param>
        public void GetBlocks(byte[] blocks)
        {
            if (blocks.Length != this.ChunkSize.x * this.ChunkSize.y * this.ChunkSize.z)
            {
                throw new ArgumentException("heightMap should be ChunkSize.x * ChunkSize.y elements long.", "heightMap");
            }

            for (int z = 0; z < this.ChunkSize.z; z++)
            {
                for (int y = 0; y < this.ChunkSize.y; y++)
                {
                    for (int x = 0; x < this.ChunkSize.x; x++)
                    {
                        int Index = z + y * this.ChunkSize.z + x * this.ChunkSize.z * this.ChunkSize.y;

                        blocks[Index] = this.BlockTypes[z, y, x];
                    }
                }
            }
        }

        /// <summary>
        /// Stores the block subtype array in Minecraft format.
        /// </summary>
        /// <param name="data">Array in which to store the block subtypes.
        /// Data will be stored as packed nibbles.</param>
        public void GetBlockData(byte[] data)
        {
            this.Pack3DArray(this.BlockSubTypes, this.ChunkSize, data);
        }

        /// <summary>
        /// Stores the block lighting array in Minecraft format.
        /// </summary>
        /// <param name="data">Array in which to store the block lighting.
        /// Data will be stored as packed nibbles.</param>
        public void GetBlockLighting(byte[] data)
        {
            this.Pack3DArray(this.BlockLight, this.ChunkSize, data);
        }

        /// <summary>
        /// Stores the block sky lighting array in Minecraft format.
        /// </summary>
        /// <param name="data">Array in which to store the block sky lighting.
        /// Data will be stored as packed nibbles.</param>
        public void GetSkyLighting(byte[] data)
        {
            this.Pack3DArray(this.SkyLight, this.ChunkSize, data);
        }

        /// <summary>
        /// Packs the 3d array into an array of nibbles.
        /// </summary>
        /// <param name="data">3d array of bytes.</param>
        /// <param name="size">Size of the 3d array.</param>
        /// <param name="packedData">Array in which to store the packed nibbles.</param>
        private void Pack3DArray(byte[, ,] data, Vector3I size, byte[] packedData)
        {
            if (packedData.Length != (size.x * size.y * size.z) / 2)
            {
                throw new ArgumentException("packedData should be size.x * size.y * size.x / 2 elements long.", "packedData");
            }

            for (int z = 0; z < size.z; z++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int x = 0; x < size.x; x++)
                    {
                        int Index = z + y * size.z + x * size.z * size.y;
                        bool UseMSB = Index % 2 == 1;
                        Index /= 2;

                        if (UseMSB)
                        {
                            packedData[Index] = (byte)((packedData[Index] & 0x0F) | (data[z, y, x] << 4));
                        }
                        else
                        {
                            packedData[Index] = (byte)((packedData[Index] & 0xF0) | (data[z, y, x] & 0x0F));
                        }
                    }
                }
            };
        }

        public void SetBlock(Vector3I localCoordinates, ItemType blockType)
        {
            if (this.NeedsDecompression) { this.Decompress(); }
            if (!this.MemoryAllocated) { this.AllocateMemory(); }
            this.NeedsCompression = true;
            this.BlockTypes[localCoordinates.z, localCoordinates.y, localCoordinates.x] = (byte)blockType;
        }

        public void SetBlock(Vector3I localCoordinates, ItemType blockType, byte blockSubType)
        {
            if (this.NeedsDecompression) { this.Decompress(); }
            if (!this.MemoryAllocated) { this.AllocateMemory(); }
            this.NeedsCompression = true;
            this.BlockTypes[localCoordinates.z, localCoordinates.y, localCoordinates.x] = (byte)blockType;
            this.BlockSubTypes[localCoordinates.z, localCoordinates.y, localCoordinates.x] = blockSubType;
        }

        /// <summary>
        /// Sets block, block subtype and lighting data.
        /// </summary>
        /// <param name="data">Block, subtype, block light and skylight data in Minecraft MapChunk format.</param>
        /// <param name="pos">Cuboid position within the chunk</param>
        /// <param name="size">Size of cuboid to update</param>
        public void SetRawData(byte[] data, Vector3I pos, Vector3I size)
        {
            if (this.NeedsDecompression) { this.Decompress(); }
            if (!this.MemoryAllocated) { this.AllocateMemory(); }
            this.NeedsCompression = true;

            int DataZ = 0;
            for (int z = pos.z; z < pos.z + size.z; z++)
            {
                int DataY = 0;
                for (int y = pos.y; y < pos.y + size.y; y++)
                {
                    int DataX = 0;
                    for (int x = pos.x; x < pos.x + size.x; x++)
                    {
                        if (x >= this.ChunkSize.x || y >= this.ChunkSize.y || z >= this.ChunkSize.z)
                        {
                            Debug.WriteLine("Warning: Tried to set block at position (" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ") outside the chunk bounds.");
                            continue;
                        }
                        int DataIndex = DataZ + DataY * size.z + DataX * size.z * size.y;
                        this.BlockTypes[z, y, x] = data[DataIndex];
                        this.UpdateBlock(new Vector3I(x, y, z));

                        bool UseMSB = DataIndex % 2 == 1;
                        DataIndex /= 2;
                        int HalfOffset = (size.x * size.y * size.z) / 2;

                        byte SubType = data[DataIndex + 2 * HalfOffset];
                        byte BlockLight = data[DataIndex + 3 * HalfOffset];
                        byte SkyLight = data[DataIndex + 4 * HalfOffset];

                        if (UseMSB)
                        {
                            SubType >>= 4;
                            SkyLight >>= 4;
                            BlockLight >>= 4;
                        }
                        else
                        {
                            SubType &= 0x0F;
                            SkyLight &= 0x0F;
                            BlockLight &= 0x0F;
                        }

                        this.BlockSubTypes[z, y, x] = SubType;
                        this.SkyLight[z, y, x] = SkyLight;
                        this.BlockLight[z, y, x] = BlockLight;

                        DataX++;
                    }

                    DataY++;
                }

                DataZ++;
            }
        }

        public int SurfaceArea
        {
            get
            {
                return this.ChunkSize.x * this.ChunkSize.y;
            }
        }

        public void UpdateBlock(Vector3I localPos)
        {
        }

        public int Volume
        {
            get
            {
                return this.ChunkSize.x * this.ChunkSize.y * this.ChunkSize.z;
            }
        }
        #endregion

        public override string ToString()
        {
            return "Chunk " + this.WorldChunkPos.ToString();
        }
    }
}
