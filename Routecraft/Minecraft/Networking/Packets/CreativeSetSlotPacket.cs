using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class CreativeSetSlotPacket : IPacket
    {
        public ushort Slot;
        public ItemPacket Item;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 2) { return false; }
            this.Slot = reader.ReadUInt16();
            if (!this.Item.Read(relay, reader)) { return false; }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt16(this.Slot);
            this.Item.Write(relay, writer);
        }
    }
}
