using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class ExplosionPacket : IPacket
    {
        public Vector3<double> Position;
        public float Radius;
        public uint BlockCount;
        public List<Vector3<sbyte>> Blocks = new List<Vector3<sbyte>>();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 32) { return false; }
            this.Position = reader.ReadDoubleVector3();
            this.Radius = reader.ReadFloat();
            this.BlockCount = reader.ReadUInt32();
            if (reader.BytesRemaining < this.BlockCount * 3) { return false; }
            for (int i = 0; i < this.BlockCount; i++)
            {
                this.Blocks.Add(reader.ReadInt8Vector3());
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteDoubleVector3(this.Position);
            writer.WriteFloat(this.Radius);
            writer.WriteUInt32(this.BlockCount);
            for (int i = 0; i < this.BlockCount; i++)
            {
                writer.WriteInt8Vector3(this.Blocks[i]);
            }
        }
    }
}
