using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EntityLookPacket : IPacket
    {
        public uint EntityID;
        public byte Yaw;
        public byte Pitch;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 6) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Yaw = reader.ReadUInt8();
            this.Pitch = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.Yaw);
            writer.WriteUInt8(this.Pitch);
        }
    }
}
