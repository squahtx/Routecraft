using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class HandshakeServerPacket : IPacket
    {
        public string Hash;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (!reader.CanReadString16) { return false; }
            this.Hash = reader.ReadString16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteString16(this.Hash);
        }
    }
}
