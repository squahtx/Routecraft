using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class EntityEffectPacket : IPacket
    {
        public uint EntityID;
        public byte EffectID;
        public byte Amplifier;
        public ushort Duration;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 8) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.EffectID = reader.ReadUInt8();
            this.Amplifier = reader.ReadUInt8();
            this.Duration = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.EffectID);
            writer.WriteUInt8(this.Amplifier);
            writer.WriteUInt16(this.Duration);
        }
    }
}
