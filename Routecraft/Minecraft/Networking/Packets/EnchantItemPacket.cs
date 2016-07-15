using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EnchantItemPacket : IPacket
    {
        public byte WindowID;
        public byte Enchantment;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 2) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.Enchantment = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteUInt8(this.Enchantment);
        }
    }
}
