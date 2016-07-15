using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class SpawnPositionPacket : IPacket
    {
        public Vector3<int> SpawnPosition = new Vector3<int>();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 12) { return false; }
            this.SpawnPosition = reader.ReadInt32Vector3();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32Vector3(this.SpawnPosition);
        }
    }
}
