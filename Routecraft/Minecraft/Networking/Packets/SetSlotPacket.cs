using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class SetSlotPacket : IPacket
    {
        public byte WindowID;
        public short Slot;
        public ItemPacket Item = new ItemPacket();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.Slot = reader.ReadInt16();
            if (!this.Item.Read(relay, reader)) { return false; }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteInt16(this.Slot);
            this.Item.Write(relay, writer);
        }
    }
}
