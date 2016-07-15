using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class SleepPacket : IPacket
    {
        public uint EntityID;
        public byte InBed;
        public int X;
        public byte Y;
        public int Z;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 14) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.InBed = reader.ReadUInt8();
            this.X = reader.ReadInt32();
            this.Y = reader.ReadUInt8();
            this.Z = reader.ReadInt32();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.InBed);
            writer.WriteInt32(this.X);
            writer.WriteUInt8(this.Y);
            writer.WriteInt32(this.Z);
        }
    }
}
