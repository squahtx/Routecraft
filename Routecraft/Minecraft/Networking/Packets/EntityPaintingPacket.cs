using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EntityPaintingPacket : IPacket
    {
        public uint EntityID;
        public string Type;
        public Vector3<int> Position;
        public uint Direction;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 22) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Type = reader.ReadString16();
            if (reader.BytesRemaining < 16) { return false; }
            this.Position = reader.ReadInt32Vector3();
            this.Direction = reader.ReadUInt32();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteString16(this.Type);
            writer.WriteInt32Vector3(this.Position);
            writer.WriteUInt32(this.Direction);
        }
    }
}
