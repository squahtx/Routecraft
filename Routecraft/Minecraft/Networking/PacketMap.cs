using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft.Networking
{
    public class PacketMap
    {
        private Dictionary<PacketType, Type> Packets = new Dictionary<PacketType, Type>();

        public PacketMap() { }

        public void Add(PacketType packetType, Type packetClass)
        {
            if (this.Packets.ContainsKey(packetType))
            {
                this.Packets.Remove(packetType);
            }
            this.Packets.Add(packetType, packetClass); 
        }

        public IPacket Create(PacketType packetType)
        {
            if (!this.Packets.ContainsKey(packetType))
            {
                return null;
            }
            return (IPacket)this.Packets[packetType].GetConstructor(new Type[0]).Invoke(new object[0]);
        }
    }
}
