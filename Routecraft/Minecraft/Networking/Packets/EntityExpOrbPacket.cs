using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EntityExpOrbPacket : IPacket
    {
        public uint EntityID;
        public Vector3<int> Position;
        public ushort Count;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 18) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Position = reader.ReadInt32Vector3();
            this.Count = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteInt32Vector3(this.Position);
            writer.WriteUInt16(this.Count);
        }
    }
}
