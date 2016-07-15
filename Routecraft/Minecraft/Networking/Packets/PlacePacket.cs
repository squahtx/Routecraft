using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PlacePacket : IPacket
    {
        public int X;
        public byte Y;
        public int Z;
        public byte Direction;
        public ItemPacket Item = new ItemPacket();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 12) { return false; }
            this.X = reader.ReadInt32();
            this.Y = reader.ReadUInt8();
            this.Z = reader.ReadInt32();
            this.Direction = reader.ReadUInt8();
            if (!this.Item.Read(relay, reader)) { return false; }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32(this.X);
            writer.WriteUInt8(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteUInt8(this.Direction);
            this.Item.Write(relay, writer);
        }
    }
}
