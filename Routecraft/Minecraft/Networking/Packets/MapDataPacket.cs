using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class MapDataPacket : IPacket
    {
        public ushort ItemID;
        public ushort Damage;
        public byte DataSize;
        public byte[] Data;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.ItemID = reader.ReadUInt16();
            this.Damage = reader.ReadUInt16();
            this.DataSize = reader.ReadUInt8();
            if (reader.BytesRemaining < this.DataSize) { return false; }
            this.Data = reader.ReadBytes(this.DataSize);

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt16(this.ItemID);
            writer.WriteUInt16(this.Damage);
            writer.WriteUInt8(this.DataSize);
            writer.WriteBytes(this.Data, this.DataSize);
        }
    }
}
