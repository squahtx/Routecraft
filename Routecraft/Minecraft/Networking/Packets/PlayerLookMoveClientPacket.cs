﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class PlayerLookMoveClientPacket : IPacket
    {
        public Vector3<double> Position = new Vector3<double>();
        public double Stance;
        public float Yaw;
        public float Pitch;
        public byte OnGround;

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 33) { return false; }
            this.Position.x = reader.ReadDouble();
            this.Position.y = reader.ReadDouble();
            this.Stance = reader.ReadDouble();
            this.Position.z = reader.ReadDouble();
            this.Yaw = reader.ReadFloat();
            this.Pitch = reader.ReadFloat();
            this.OnGround = reader.ReadUInt8();
            if (this.OnGround > 1)
            {
                throw new InvalidDataException();
            }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteDouble(this.Position.x);
            writer.WriteDouble(this.Position.y);
            writer.WriteDouble(this.Stance);
            writer.WriteDouble(this.Position.z);
            writer.WriteFloat(this.Yaw);
            writer.WriteFloat(this.Pitch);
            writer.WriteUInt8(this.OnGround);
        }
    }
}
