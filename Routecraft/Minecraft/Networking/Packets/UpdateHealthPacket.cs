using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class UpdateHealthPacket : IPacket
    {
        public ushort Health;
        public ushort Food;
        public float FoodSaturation;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 8) { return false; }
            this.Health = reader.ReadUInt16();
            this.Food = reader.ReadUInt16();
            this.FoodSaturation = reader.ReadFloat();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt16(this.Health);
            writer.WriteUInt16(this.Food);
            writer.WriteFloat(this.FoodSaturation);
        }
    }
}
