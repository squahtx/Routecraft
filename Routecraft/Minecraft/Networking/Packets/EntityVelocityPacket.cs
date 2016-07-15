using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EntityVelocityPacket : IPacket
    {
        public uint EntityID;
        public Vector3<short> Velocity;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 10) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Velocity = reader.ReadInt16Vector3();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteInt16Vector3(this.Velocity);
        }
    }
}
