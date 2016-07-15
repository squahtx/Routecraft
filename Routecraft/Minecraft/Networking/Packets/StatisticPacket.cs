using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class StatisticPacket : IPacket
    {
        public uint StatisticID;
        public byte Amount;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.StatisticID = reader.ReadUInt32();
            this.Amount = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.StatisticID);
            writer.WriteUInt8(this.Amount);
        }
    }
}
