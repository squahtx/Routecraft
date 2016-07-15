using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class RespawnPacket : IPacket
    {
        public DimensionType Dimension;
        public byte Difficulty;
        public byte Gamemode;
        public ushort WorldHeight;
        public long Seed;
        public string LevelType;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 15) { return false; }
            this.Dimension = (DimensionType)reader.ReadInt8();
            this.Difficulty = reader.ReadUInt8();
            this.Gamemode = reader.ReadUInt8();
            this.WorldHeight = reader.ReadUInt16();
            this.Seed = reader.ReadInt64();
            if (!reader.CanReadString16) { return false; }
            this.LevelType = reader.ReadString16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt8((sbyte)this.Dimension);
            writer.WriteUInt8(this.Difficulty);
            writer.WriteUInt8(this.Gamemode);
            writer.WriteUInt16(this.WorldHeight);
            writer.WriteInt64(this.Seed);
            writer.WriteString16(this.LevelType);
        }
    }
}
