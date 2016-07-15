using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class AnimationPacket : IPacket
    {
        public uint EntityID;
        public byte AnimationID;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 5) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.AnimationID = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.AnimationID);
        }
    }
}
