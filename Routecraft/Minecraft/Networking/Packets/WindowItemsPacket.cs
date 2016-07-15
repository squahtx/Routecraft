using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class WindowItemsPacket : IPacket
    {
        public byte WindowID;
        public ushort ItemCount;
        public List<ItemPacket> Items = new List<ItemPacket>();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 3) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.ItemCount = reader.ReadUInt16();

            for (int i = 0; i < this.ItemCount; i++)
            {
                ItemPacket Item = new ItemPacket();
                if (!Item.Read(relay, reader)) { return false; }
                this.Items.Add(Item);
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteUInt16(this.ItemCount);

            foreach (ItemPacket Item in Items)
            {
                Item.Write(relay, writer);
            }
        }
    }
}
