using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class UpdateTimePacket : IPacket
    {
        public ulong Ticks;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 8) { return false; }
            this.Ticks = reader.ReadUInt64();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt64(this.Ticks);
        }
    }
}
