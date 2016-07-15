using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking;

namespace Routecraft.Minecraft
{
    public class MinecraftDatabase
    {
        public MinecraftItemDatabase Items { get; private set; }
        public PacketMap ClientPacketMap { get; private set; }
        public PacketMap ServerPacketMap { get; private set; }

        public MinecraftDatabase()
        {
            this.Items = new MinecraftItemDatabase();
            this.ClientPacketMap = new ClientPacketMap();
            this.ServerPacketMap = new ServerPacketMap();
        }
    }
}
