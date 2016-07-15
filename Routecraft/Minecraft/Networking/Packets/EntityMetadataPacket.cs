using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EntityMetadataPacket : IPacket
    {
        public uint EntityID;
        public Metadata Metadata = new Metadata();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.EntityID = reader.ReadUInt32();
            if (!this.Metadata.Read(relay, reader)) { return false; }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            this.Metadata.Write(relay, writer);
        }
    }
}
