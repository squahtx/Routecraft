using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PickupSpawnPacket : IPacket
    {
        public uint EntityID;
        public ushort ItemID;
        public byte ItemCount;
        public ushort Damage;
        public Vector3<int> ItemPosition = new Vector3<int>();
        public byte Rotation;
        public byte Pitch;
        public byte Roll;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 24) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.ItemID = reader.ReadUInt16();
            this.ItemCount = reader.ReadUInt8();
            this.Damage = reader.ReadUInt16();
            this.ItemPosition = reader.ReadInt32Vector3();
            this.Rotation = reader.ReadUInt8();
            this.Pitch = reader.ReadUInt8();
            this.Roll = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt16(this.ItemID);
            writer.WriteUInt8(this.ItemCount);
            writer.WriteUInt16(this.Damage);
            writer.WriteInt32Vector3(this.ItemPosition);
            writer.WriteUInt8(this.Rotation);
            writer.WriteUInt8(this.Pitch);
            writer.WriteUInt8(this.Roll);
        }
    }
}
