using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class ExperiencePacket : IPacket
    {
        public float ExperienceBar;
        public ushort Level;
        public ushort TotalExperience;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 8) { return false; }
            this.ExperienceBar = reader.ReadFloat();
            this.Level = reader.ReadUInt16();
            this.TotalExperience = reader.ReadUInt16();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteFloat(this.ExperienceBar);
            writer.WriteUInt16(this.Level);
            writer.WriteUInt16(this.TotalExperience);
        }
    }
}
