using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PlayerInventoryPacket : IPacket
    {
        public uint EntityID;
        public ushort Slot;
        public short ItemID;
        public ushort Damage;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 10) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Slot = reader.ReadUInt16();
            this.ItemID = reader.ReadInt16();
            this.Damage = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt16(this.Slot);
            writer.WriteInt16(this.ItemID);
            writer.WriteUInt16(this.Damage);
        }
    }
}
