using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Routecraft.Minecraft.Networking;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft
{
    public class WorldBackupCreator
    {
        private Relay Relay;
        private World World;
        private Dimension CurrentDimension;

        private WorkerThread WorkerThread = new WorkerThread();

        public WorldBackupCreator()
        {
        }

        public void Initialize(Relay relay)
        {
            this.Relay = relay;

            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.LoginAccepted, this.LoginAcceptedHandler);
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.SpawnPosition, this.SpawnPositionHandler);
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.Respawn, this.RespawnHandler);
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.PreChunk, this.PreChunkHandler);
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.MapChunk, this.MapChunkHandler);
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.MultiBlockChange, this.MultiBlockChangeHandler);
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.BlockChange, this.BlockChangeHandler);

            this.Relay.RelayClosed += this.RelayClosedHandler;

            this.WorkerThread.Start();
        }

        public void Uninitialize()
        {
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.LoginAccepted, this.LoginAcceptedHandler);
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.SpawnPosition, this.SpawnPositionHandler);
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.Respawn, this.RespawnHandler);
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.PreChunk, this.PreChunkHandler);
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.MapChunk, this.MapChunkHandler);
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.MultiBlockChange, this.MultiBlockChangeHandler);
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.BlockChange, this.BlockChangeHandler);

            this.Relay.RelayClosed -= this.RelayClosedHandler;

            if (this.World != null)
            {
                this.WorkerThread.AddJob(delegate()
                {
                    this.World.Save();
                    this.World = null;
                });
            }

            this.WorkerThread.Stop();
        }

        private void SetActiveDimension(DimensionType dimension)
        {
            if (this.CurrentDimension != null && this.CurrentDimension.Type == dimension) { return; }

            if (this.CurrentDimension != null)
            {
                int ChunkCount = 0;
                foreach (Chunk Chunk in this.CurrentDimension.LoadedChunks.ToArray())
                {
                    this.CurrentDimension.UnloadChunk(Chunk.WorldChunkPos);
                    ChunkCount++;
                }
                Debug.WriteLine("Unloaded " + ChunkCount.ToString() + " chunks in dimension " + this.CurrentDimension.Type.ToString() + ".");
                this.CurrentDimension = null;
            }

            this.CurrentDimension = this.World.GetDimension(dimension);
            Debug.WriteLine("Dimension set to " + this.CurrentDimension.Type.ToString() + ".");
        }

        private void RelayClosedHandler(Relay relay)
        {
            this.Uninitialize();
        }

        #region Packet Handlers
        private MinecraftRelayAction LoginAcceptedHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            LoginAcceptedPacket Packet = (LoginAcceptedPacket)packet;

            if (this.World != null)
            {
                throw new InvalidDataException();
            }
            this.World = new World("C:\\Users\\Sean\\Desktop\\Minecraft Server\\world");

            this.World.Seed = Packet.Seed;
            this.World.CreativeMode = Packet.Mode == 1;
            this.SetActiveDimension((DimensionType)Packet.Dimension);

            return MinecraftRelayAction.Relay;
        }

        private MinecraftRelayAction RespawnHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            RespawnPacket Packet = (RespawnPacket)packet;

            this.WorkerThread.AddJob(delegate()
            {
                this.SetActiveDimension((DimensionType)Packet.Dimension);
            });

            return MinecraftRelayAction.Relay;
        }

        private MinecraftRelayAction SpawnPositionHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            SpawnPositionPacket Packet = (SpawnPositionPacket)packet;
            this.World.SpawnPos = new Vector3I(Packet.SpawnPosition.x, Packet.SpawnPosition.z, Packet.SpawnPosition.y);

            return MinecraftRelayAction.Relay;
        }

        private MinecraftRelayAction PreChunkHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            PreChunkPacket Packet = (PreChunkPacket)packet;

            this.WorkerThread.AddJob(delegate()
            {
                if (Packet.Load == 1)
                {
                    if (!this.CurrentDimension.IsChunkLoaded(Packet.ChunkPosition))
                    {
                        this.CurrentDimension.LoadChunk(Packet.ChunkPosition);
                    }
                }
                else
                {
                    this.CurrentDimension.UnloadChunk(Packet.ChunkPosition);
                }
            });

            return MinecraftRelayAction.Relay;
        }

        private static byte[] DecompressedChunkData = new byte[16 * 16 * 128 * 5 / 2];
        private MinecraftRelayAction MapChunkHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            MapChunkPacket Packet = (MapChunkPacket)packet;

            this.WorkerThread.AddJob(delegate()
            {
                Vector3I Coordinates = new Vector3I(Packet.X, Packet.Z, Packet.Y);
                Vector2I ChunkCoordinates = this.CurrentDimension.WorldPosToChunkPos(Coordinates);
                if (!this.CurrentDimension.IsChunkLoaded(ChunkCoordinates))
                {
                    Debug.WriteLine("Discarding incoming data for unloaded chunk " + ChunkCoordinates.ToString() + ".");
                    return;
                }

                lock (WorldBackupCreator.DecompressedChunkData)
                {
                    Packet.GetDecompressedData(WorldBackupCreator.DecompressedChunkData);
                    Vector3I LocalCoordinates = this.CurrentDimension.WorldPosToChunkLocalPos(Coordinates);
                    Chunk Chunk = this.CurrentDimension.GetChunk(ChunkCoordinates);
                    Vector3I Size = new Vector3I(Packet.Size.x + 1, Packet.Size.z + 1, Packet.Size.y + 1);

                    Chunk.SetRawData(WorldBackupCreator.DecompressedChunkData, LocalCoordinates, Size);
                }
            });

            return MinecraftRelayAction.Relay;
        }

        private MinecraftRelayAction MultiBlockChangeHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            MultiBlockChangePacket Packet = (MultiBlockChangePacket)packet;

            this.WorkerThread.AddJob(delegate()
            {
                Vector2I ChunkCoordinates = Packet.ChunkPosition;
                Chunk Chunk = this.CurrentDimension.GetChunk(ChunkCoordinates);

                for (int i = 0; i < Packet.BlockCount; i++)
                {
                    Vector3I LocalCoordinates = new Vector3I(Packet.Coordinates[2 * i] >> 4, Packet.Coordinates[2 * i] & 0x0F, Packet.Coordinates[2 * i + 1]);
                    Chunk.SetBlock(LocalCoordinates, (ItemType)Packet.Types[i], Packet.Metadata[i]);
                }
            });

            return MinecraftRelayAction.Relay;
        }

        private MinecraftRelayAction BlockChangeHandler(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            BlockChangePacket Packet = (BlockChangePacket)packet;

            this.WorkerThread.AddJob(delegate()
            {
                Vector3I Coordinates = new Vector3I(Packet.X, Packet.Z, Packet.Y);
                Vector2I ChunkCoordinates = this.CurrentDimension.WorldPosToChunkPos(Coordinates);
                Vector3I LocalCoordinates = this.CurrentDimension.WorldPosToChunkLocalPos(Coordinates);
                Chunk Chunk = this.CurrentDimension.GetChunk(ChunkCoordinates);
                Chunk.SetBlock(LocalCoordinates, (ItemType)Packet.Type, Packet.Metadata);
            });

            return MinecraftRelayAction.Relay;
        }
        #endregion
    }
}
