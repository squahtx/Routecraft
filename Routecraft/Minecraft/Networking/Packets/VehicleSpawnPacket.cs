using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class VehicleSpawnPacket : IPacket
    {
        public uint EntityID;
        public byte Type;
        public Vector3<int> Position;
        public uint FireballThrowerEntityID;
        public Vector3<short> Unknown;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 22) { return false; }
            this.EntityID = reader.ReadUInt32();
            this.Type = reader.ReadUInt8();
            this.Position = reader.ReadInt32Vector3();
            this.FireballThrowerEntityID = reader.ReadUInt32();
            if (this.FireballThrowerEntityID > 0)
            {
                if (reader.BytesRemaining < 6) { return false; }
                this.Unknown = reader.ReadInt16Vector3();
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt32(this.EntityID);
            writer.WriteUInt8(this.Type);
            writer.WriteInt32Vector3(this.Position);
            writer.WriteUInt32(this.FireballThrowerEntityID);
            if (this.FireballThrowerEntityID > 0)
            {
                writer.WriteInt16Vector3(this.Unknown);
            }
        }
    }
}
