using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Networking.Packets
{
    public class WindowClickPacket : IPacket
    {
        public byte WindowID;
        public ushort Slot;
        public byte RightClick;
        public ushort ActionID;
        public byte Shift;
        public ItemPacket Item = new ItemPacket();

        public bool Read(MinecraftRelay relay, DataReader reader)
        {
            if (reader.BytesRemaining < 9) { return false; }
            this.WindowID = reader.ReadUInt8();
            this.Slot = reader.ReadUInt16();
            this.RightClick = reader.ReadUInt8();
            this.ActionID = reader.ReadUInt16();
            this.Shift = reader.ReadUInt8();
            if (!this.Item.Read(relay, reader)) { return false; }

            return true;
        }

        public void Write(MinecraftRelay relay, DataWriter writer)
        {
            writer.WriteUInt8(this.WindowID);
            writer.WriteUInt16(this.Slot);
            writer.WriteUInt8(this.RightClick);
            writer.WriteUInt16(this.ActionID);
            writer.WriteUInt8(this.Shift);
            this.Item.Write(relay, writer);
        }
    }
}
