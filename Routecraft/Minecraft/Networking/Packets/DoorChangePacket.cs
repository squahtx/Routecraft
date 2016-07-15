using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class DoorChangePacket : IPacket
    {
        public uint EffectID;
        public int X;
        public byte Y;
        public int Z;
        public uint Data;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 17) { return false; }
            this.EffectID = reader.ReadUInt32();
            this.X = reader.ReadInt32();
            this.Y = reader.ReadUInt8();
            this.Z = reader.ReadInt32();
            this.Data = reader.ReadUInt32();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EffectID);
            writer.WriteInt32(this.X);
            writer.WriteUInt8(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteUInt32(this.Data);
        }
    }
}
