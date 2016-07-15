using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft.Networking
{
    public class Metadata : List<byte>, IPacket
    {
        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            byte x = 0;
            while (true)
            {
                if (reader.BytesRemaining < 1) { return false; }
                x = reader.ReadUInt8();
                this.Add(x);
                if (x == 127)
                {
                    break;
                }
                switch (x >> 5)
                {
                    case 0:
                        if (reader.BytesRemaining < 1) { return false; }
                        this.Add(reader.ReadUInt8());
                        break;
                    case 1:
                        if (reader.BytesRemaining < 2) { return false; }
                        this.AddRange(reader.ReadBytes(2));
                        break;
                    case 2:
                        if (reader.BytesRemaining < 4) { return false; }
                        this.AddRange(reader.ReadBytes(4));
                        break;
                    case 3:
                        if (reader.BytesRemaining < 4) { return false; }
                        this.AddRange(reader.ReadBytes(4));
                        break;
                    case 4:
                        // UTF16 string
                        if (!reader.CanReadString16) { return false; }
                        ushort CharCount = reader.ReadUInt16();
                        reader.Position -= 2;
                        this.AddRange(reader.ReadBytes(2 + CharCount * 2));
                        break;
                    case 5:
                        if (reader.BytesRemaining < 5) { return false; }
                        this.AddRange(reader.ReadBytes(5));
                        break;
                    case 6:
                        if (reader.BytesRemaining < 12) { return false; }
                        this.AddRange(reader.ReadBytes(12));
                        break;
                }
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteBytes(this);
        }
    }
}
