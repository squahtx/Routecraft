using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PreChunkPacket : IPacket
    {
        public Vector2<int> ChunkPosition;
        public byte Load;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 9) { return false; }
            this.ChunkPosition = reader.ReadInt32Vector2();
            this.Load = reader.ReadUInt8();
            if (this.Load > 1)
            {
                throw new InvalidDataException();
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteInt32Vector2(this.ChunkPosition);
            writer.WriteUInt8(this.Load);
        }
    }
}
