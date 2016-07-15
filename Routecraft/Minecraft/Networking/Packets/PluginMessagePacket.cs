using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PluginMessagePacket : IPacket
    {
        public string Channel;
        public ushort Length;
        public byte[] Data;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 4) { return false; }
            if (!reader.CanReadString16) { return false; }
            this.Channel = reader.ReadString16();
            if (reader.BytesRemaining < 2) { return false; }
            this.Length = reader.ReadUInt16();
            if (reader.BytesRemaining < this.Length) { return false; }
            this.Data = reader.ReadBytes(this.Length);

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteString16(this.Channel);
            writer.WriteUInt16(this.Length);
            writer.WriteBytes(this.Data);
        }
    }
}
