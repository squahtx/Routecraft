using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft.PacketHandlers
{
    public class RelayPacketHandler
    {
        public RelayPacketHandler()
        {
        }

        public virtual MinecraftRelayAction HandlePacket (MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            return MinecraftRelayAction.Relay;
        }
    }
}
