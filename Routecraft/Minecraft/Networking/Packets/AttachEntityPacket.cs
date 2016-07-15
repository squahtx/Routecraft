using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class AttachEntityPacket : IPacket
    {
        public uint EntityID;
        public uint VehicleID;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 8) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.VehicleID = reader.ReadUInt32();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt32(this.VehicleID);
        }
    }
}
