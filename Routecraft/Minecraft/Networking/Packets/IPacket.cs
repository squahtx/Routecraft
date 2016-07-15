using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public interface IPacket
    {
        bool Read(MinecraftRelay relay, DataReader reader);
        void Write(MinecraftRelay relay, DataWriter writer);
    }
}
