using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class TransactionPacket : IPacket
    {
        public byte WindowID;
        public ushort ActionID;
        public byte Accepted;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 4) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.ActionID = reader.ReadUInt16();
            this.Accepted = reader.ReadUInt8();
            if (this.Accepted > 1)
            {
                throw new InvalidDataException();
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteUInt16(this.ActionID);
            writer.WriteUInt8(this.Accepted);
        }
    }
}
