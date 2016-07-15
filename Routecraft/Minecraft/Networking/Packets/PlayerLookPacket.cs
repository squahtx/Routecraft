using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PlayerLookPacket : IPacket
    {
        public float Yaw;
        public float Pitch;
        public byte OnGround;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 9) { return false; }
            this.Yaw = reader.ReadFloat();
            this.Pitch = reader.ReadFloat();
            this.OnGround = reader.ReadUInt8();
            if (this.OnGround > 1)
            {
                throw new InvalidDataException();
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteFloat(this.Yaw);
            writer.WriteFloat(this.Pitch);
            writer.WriteUInt8(this.OnGround);
        }
    }
}
