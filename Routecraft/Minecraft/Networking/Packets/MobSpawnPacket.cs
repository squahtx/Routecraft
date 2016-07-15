using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class MobSpawnPacket : IPacket
    {
        public uint EntityID;
        public byte Type;
        public Vector3<int> Position;
        public sbyte Yaw;
        public sbyte Pitch;
        public Metadata Metadata = new Metadata();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 20) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Type = reader.ReadUInt8();
            this.Position = reader.ReadInt32Vector3();
            this.Yaw = reader.ReadInt8();
            this.Pitch = reader.ReadInt8();
            if (!this.Metadata.Read(relay, reader)) { return false; }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.Type);
            writer.WriteInt32Vector3(this.Position);
            writer.WriteInt8(this.Yaw);
            writer.WriteInt8(this.Pitch);
            this.Metadata.Write(relay, writer);
        }
    }
}
