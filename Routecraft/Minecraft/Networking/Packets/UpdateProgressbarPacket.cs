using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class UpdateProgressbarPacket : IPacket
    {
        public byte WindowID;
        public ushort ProgressBarID;
        public ushort Value;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.ProgressBarID = reader.ReadUInt16();
            this.Value = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteUInt16(this.ProgressBarID);
            writer.WriteUInt16(this.Value);
        }
    }
}
