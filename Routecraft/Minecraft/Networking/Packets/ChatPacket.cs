using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class ChatPacket : IPacket
    {
        public string Message;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (!reader.CanReadString16) { return false; }
            this.Message = reader.ReadString16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteString16(this.Message);
        }
    }
}
