using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class WeatherPacket : IPacket
    {
        public uint EntityID;
        public byte Unknown;
        public Vector3<int> Position;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 17) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Unknown = reader.ReadUInt8();
            this.Position = reader.ReadInt32Vector3();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.Unknown);
            writer.WriteInt32Vector3(this.Position);
        }
    }
}
