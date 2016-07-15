using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class MultiBlockChangePacket : IPacket
    {
        public Vector2<int> ChunkPosition;
        public ushort BlockCount;
        public byte[] Coordinates;
        public byte[] Types;
        public byte[] Metadata;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 10) { return false; }
            this.ChunkPosition = reader.ReadInt32Vector2();
            this.BlockCount = reader.ReadUInt16();
            if (reader.BytesRemaining < this.BlockCount * 4) { return false; }
            this.Coordinates = reader.ReadBytes(this.BlockCount * 2);
            this.Types = reader.ReadBytes(this.BlockCount);
            this.Metadata = reader.ReadBytes(this.BlockCount);

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32Vector2(this.ChunkPosition);
            writer.WriteUInt16(this.BlockCount);
            writer.WriteBytes(this.Coordinates, this.BlockCount * 2);
            writer.WriteBytes(this.Types, this.BlockCount);
            writer.WriteBytes(this.Metadata, this.BlockCount);
        }
    }
}
