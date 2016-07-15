using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class UpdateSignPacket : IPacket
    {
        public int X;
        public short Y;
        public int Z;
        public string Line1;
        public string Line2;
        public string Line3;
        public string Line4;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 10) { return false; }
            this.X = reader.ReadInt32();
            this.Y = reader.ReadInt16();
            this.Z = reader.ReadInt32();
            if (!reader.CanReadString16) { return false; }
            this.Line1 = reader.ReadString16();
            if (!reader.CanReadString16) { return false; }
            this.Line2 = reader.ReadString16();
            if (!reader.CanReadString16) { return false; }
            this.Line3 = reader.ReadString16();
            if (!reader.CanReadString16) { return false; }
            this.Line4 = reader.ReadString16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32(this.X);
            writer.WriteInt16(this.Y);
            writer.WriteInt32(this.Z);
            writer.WriteString16(this.Line1);
            writer.WriteString16(this.Line2);
            writer.WriteString16(this.Line3);
            writer.WriteString16(this.Line4);
        }
    }
}
