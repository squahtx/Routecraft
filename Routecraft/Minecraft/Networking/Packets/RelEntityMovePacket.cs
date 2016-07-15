using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class RelEntityMovePacket : IPacket
    {
        public uint EntityID;
        public Vector3<sbyte> DeltaPosition;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 7) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.DeltaPosition = reader.ReadInt8Vector3();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteInt8Vector3(this.DeltaPosition);
        }
    }
}
