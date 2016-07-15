using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class BedPacket : IPacket
    {
        public byte Action;
        public byte Gamemode;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 2) { return false; }
            this.Action = reader.ReadUInt8();
            this.Gamemode = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.Action);
            writer.WriteUInt8(this.Gamemode);
        }
    }
}
