using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class NamedEntitySpawnPacket : IPacket
    {
        public uint EntityID;
        public string Name;
        public Vector3<int> Position;
        public byte Rotation;
        public byte Pitch;
        public ushort CurrentItem;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 22) { return false; }
            this.EntityID = reader.ReadUInt32();
            if (!reader.CanReadString16) { return false; }
            this.Name = reader.ReadString16();
            if (reader.BytesRemaining < 16) { return false; }
            this.Position = reader.ReadInt32Vector3();
            this.Rotation = reader.ReadUInt8();
            this.Pitch = reader.ReadUInt8();
            this.CurrentItem = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteString16(this.Name);
            writer.WriteInt32Vector3(this.Position);
            writer.WriteUInt8(this.Rotation);
            writer.WriteUInt8(this.Pitch);
            writer.WriteUInt16(this.CurrentItem);
        }
    }
}
