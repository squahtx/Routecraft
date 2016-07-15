using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class UseEntityPacket : IPacket
    {
        public uint PlayerEntityID;
        public uint TargetEntityID;
        public byte LeftClick;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 9) { return false; }
            this.PlayerEntityID = reader.ReadUInt32();
            this.TargetEntityID = reader.ReadUInt32();
            this.LeftClick = reader.ReadUInt8();

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.PlayerEntityID);
            writer.WriteUInt32(this.TargetEntityID);
            writer.WriteUInt8(this.LeftClick);
        }
    }
}
