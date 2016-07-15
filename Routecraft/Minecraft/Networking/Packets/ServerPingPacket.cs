using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class ServerPingPacket : IPacket
    {
        public bool Read(MinecraftRelay relay, DataReader reader) { return true; }
        public void Write(MinecraftRelay relay, DataWriter writer) { }
    }
}
