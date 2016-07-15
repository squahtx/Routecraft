using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PlayerInfoPacket : IPacket
    {
        public string PlayerName;
        public byte Online;
        public ushort Ping;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (!reader.CanReadString16) { return false; }
            this.PlayerName = reader.ReadString16();
            if (reader.BytesRemaining < 3) { return false; }
            this.Online = reader.ReadUInt8();
            this.Ping = reader.ReadUInt16();
            if (this.Online > 1)
            {
                throw new InvalidDataException();
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteString16(this.PlayerName);
            writer.WriteUInt8(this.Online);
            writer.WriteUInt16(this.Ping);
        }
    }
}
