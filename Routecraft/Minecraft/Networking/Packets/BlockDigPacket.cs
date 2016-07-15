using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class BlockDigPacket : IPacket
    {
        public byte ActionID;
        public int X;
        public byte Y;
        public int Z;
        public byte Face;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 11) { return false; }
            this.ActionID = reader.ReadUInt8();
            this.X = reader.ReadInt32();
            this.Y = reader.ReadUInt8();
            this.Z = reader.ReadInt32();
            this.Face = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.ActionID);
            writer.WriteInt32(this.X);
            writer.WriteUInt8(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteUInt8(this.Face);
        }
    }
}
