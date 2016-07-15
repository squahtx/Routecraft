using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class OpenWindowPacket : IPacket
    {
        public byte WindowID;
        public byte InventoryType;
        public string Title;
        public byte SlotCount;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.InventoryType = reader.ReadUInt8();
            this.Title = reader.ReadString16();
            if (reader.BytesRemaining < 1) { return false; }
            this.SlotCount = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteUInt8(this.InventoryType);
            writer.WriteString16(this.Title);
            writer.WriteUInt8(this.SlotCount);
        }
    }
}
