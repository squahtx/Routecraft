using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class ItemPacket : IPacket
    {
        public ItemType ItemID;
        public byte ItemCount;
        public ushort Damage;
        public short DataLength = -1;
        public byte[] Data;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 2) { return false; }
            this.ItemID = (ItemType)reader.ReadInt16();
            if (this.ItemID != ItemType.Invalid)
            {
                if (reader.BytesRemaining < 3) { return false; }
                this.ItemCount = reader.ReadUInt8();
                this.Damage = reader.ReadUInt16();

                if (relay.MinecraftDatabase.Items.GetItem(this.ItemID).CanEnchant)
                {
                    if (reader.BytesRemaining < 2) { return false; }
                    this.DataLength = reader.ReadInt16();
                    if (this.DataLength >= 0)
                    {
                        if (reader.BytesRemaining < this.DataLength) { return false; }
                        this.Data = reader.ReadBytes(this.DataLength);
                    }
                }
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt16((short)this.ItemID);
            if (this.ItemID != ItemType.Invalid)
            {
                writer.WriteUInt8(this.ItemCount);
                writer.WriteUInt16(this.Damage);

                if (relay.MinecraftDatabase.Items.GetItem(this.ItemID).CanEnchant)
                {
                    writer.WriteInt16(this.DataLength);
                    if (this.DataLength != -1)
                    {
                        writer.WriteBytes(this.Data, this.DataLength);
                    }
                }
            }
        }
    }
}
