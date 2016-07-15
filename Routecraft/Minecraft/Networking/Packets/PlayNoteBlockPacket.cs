using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PlayNoteBlockPacket : IPacket
    {
        public int X;
        public short Y;
        public int Z;
        public byte Data1;
        public byte Data2;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 12) { return false; }
            this.X = reader.ReadInt32();
            this.Y = reader.ReadInt16();
            this.Z = reader.ReadInt32();
            this.Data1 = reader.ReadUInt8();
            this.Data2 = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32(this.X);
            writer.WriteInt16(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteUInt8(this.Data1);
            writer.WriteUInt8(this.Data2);
        }
    }
}
