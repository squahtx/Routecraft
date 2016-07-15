using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class BlockChangePacket : IPacket
    {
        public int X;
        public byte Y;
        public int Z;
        public byte Type;
        public byte Metadata;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 11) { return false; }
            this.X = reader.ReadInt32();
            this.Y = reader.ReadUInt8();
            this.Z = reader.ReadInt32();
            this.Type = reader.ReadUInt8();
            this.Metadata = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32(this.X);
            writer.WriteUInt8(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteUInt8(this.Type);
            writer.WriteUInt8(this.Metadata);
        }
    }
}
