using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class BlockItemSwitchPacket : IPacket
    {
        public ushort Slot;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 2) { return false; }
            this.Slot = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt16(this.Slot);
        }
    }
}
