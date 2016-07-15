using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class CollectPacket : IPacket
    {
        public uint CollectedEntityID;
        public uint CollectorEntityID;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 8) { return false; }
            this.CollectedEntityID = reader.ReadUInt32();
            this.CollectorEntityID = reader.ReadUInt32();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.CollectedEntityID);
            writer.WriteUInt32(this.CollectorEntityID);
        }
    }
}
