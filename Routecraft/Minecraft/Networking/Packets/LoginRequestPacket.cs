using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class LoginRequestPacket : IPacket
    {
        public uint ProtocolVersion;
        public string PlayerName;
        public long Seed;
        public string LevelType;
        public int Mode;
        public sbyte Dimension;
        public sbyte PlayerCount;
        public byte WorldHeight;
        public byte MaxPlayers;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 22) { return false; }
            this.ProtocolVersion = reader.ReadUInt32();
            if (!reader.CanReadString16) { return false; }
            this.PlayerName = reader.ReadString16();
            if (reader.BytesRemaining < 18) { return false; }
            this.Seed = reader.ReadInt64();
            if (!reader.CanReadString16) { return false; }
            this.LevelType = reader.ReadString16();
            if (reader.BytesRemaining < 8) { return false; }
            this.Mode = reader.ReadInt32();
            this.Dimension = reader.ReadInt8();
            this.PlayerCount = reader.ReadInt8();
            this.WorldHeight = reader.ReadUInt8();
            this.MaxPlayers = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.ProtocolVersion);
            writer.WriteString16(this.PlayerName);
            writer.WriteInt64(this.Seed);
            writer.WriteString16(this.LevelType);
            writer.WriteInt32(this.Mode);
            writer.WriteInt8(this.Dimension);
            writer.WriteInt8(this.PlayerCount);
            writer.WriteUInt8(this.WorldHeight);
            writer.WriteUInt8(this.MaxPlayers);
        }
    }
}
