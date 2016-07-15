using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking.Packets;
using Routecraft.Minecraft.PacketHandlers;

namespace Routecraft.Minecraft.Networking
{
    public class MinecraftRelay : IMinecraftRelay
    {
        public delegate void MinecraftRelayEvent(MinecraftRelay relay);
        public delegate MinecraftRelayAction RelayPacketEvent(MinecraftRelay relay, PacketType packetType, IPacket packet);
        public event MinecraftRelayEvent ConnectionClosed;

        public MinecraftDatabase MinecraftDatabase { get; private set; }

        private TcpRelay Relay = null;
        private List<byte> Buffer = new List<byte>();
        private DataReader Reader = new DataReader();
        private PacketMap PacketMap = null;

        private RelayPacketEvent[] PacketHandlers = new RelayPacketEvent[256];

        public MinecraftRelay(MinecraftDatabase minecraftDatabase, TcpRelay relay, PacketMap packetMap)
        {
            this.MinecraftDatabase = minecraftDatabase;

            this.Reader.Initialize(this.Buffer);

            this.Relay = relay;
            this.Relay.DataReceived += this.DataReceived;
            this.Relay.ConnectionClosed += this.OnConnectionClosed;

            this.PacketMap = packetMap;
        }

        public void AddPacketHandler(PacketType packetType, RelayPacketEvent handler)
        {
            this.PacketHandlers[(int)packetType] += handler;
        }

        public void AddPacketHandler(PacketType packetType, RelayPacketHandler handler)
        {
            this.PacketHandlers[(int)packetType] += handler.HandlePacket;
        }

        public void Close()
        {
            this.Relay.Close();
        }

        public void RemovePacketHandler(PacketType packetType)
        {
            this.PacketHandlers[(int)packetType] = null;
        }

        public void RemovePacketHandler(PacketType packetType, RelayPacketEvent handler)
        {
            this.PacketHandlers[(int)packetType] -= handler;
        }

        public void RemovePacketHandler(PacketType packetType, RelayPacketHandler handler)
        {
            this.PacketHandlers[(int)packetType] -= handler.HandlePacket;
        }

        public void QueuePacket(PacketType packetType, IPacket packet)
        {
            DataWriter Writer = new DataWriter();
            Writer.WriteUInt8((byte)packetType);
            packet.Write(this, Writer);
            this.Relay.Write(Writer.Bytes);
        }

        public void QueueRawPacket(DataWriter writer)
        {
            this.Relay.Write(writer.Bytes);
        }

        public void Start()
        {
            this.Relay.Start();
        }

        private TcpRelayAction DataReceived(TcpRelay relay, byte[] data, int size)
        {
            Buffer.AddRange(data.Take(size));
            if (Buffer.Count > 1024 * 1024)
            {
                throw new OverflowException();
            }
            while (this.ProcessPacket()) { }
            return TcpRelayAction.Drop;
        }

        private IPacket PreviousPacket = null;
        private bool ProcessPacket()
        {
            DataReader Reader = this.Reader;
            Reader.Initialize(this.Buffer);
            if (Reader.BytesRemaining < 1) { return false; }
            PacketType PacketType = (PacketType)this.Reader.ReadUInt8();
            IPacket Packet = this.PacketMap.Create(PacketType);
            if (Packet != null)
            {
                if (!Packet.Read(this, Reader))
                {
                    return false;
                }
                MinecraftRelayAction Action = MinecraftRelayAction.Relay;
                if (this.PacketHandlers[(int)PacketType] != null)
                {
                    foreach (RelayPacketEvent packetHandler in this.PacketHandlers[(int)PacketType].GetInvocationList())
                    {
                        MinecraftRelayAction HandlerAction = packetHandler(this, PacketType, Packet);
                        if (HandlerAction == MinecraftRelayAction.Drop)
                        {
                            Action = MinecraftRelayAction.Drop;
                            break;
                        }
                        if (HandlerAction == MinecraftRelayAction.RelayModified)
                        {
                            Action = MinecraftRelayAction.RelayModified;
                        }
                    }
                }
                if (Action == MinecraftRelayAction.Relay)
                {
                    DataWriter Writer = new DataWriter();
                    Writer.WriteUInt8((byte)PacketType);
                    Packet.Write(this, Writer);
                    if (!Writer.Bytes.SequenceEqual(this.Buffer.Take(Reader.Position)))
                    {
                        throw new InvalidDataException();
                    }
                    this.QueueRawPacket(Writer);
                }
                if (Action == MinecraftRelayAction.RelayModified)
                {
                    this.QueuePacket(PacketType, Packet);
                }
            }
            else
            {
                throw new InvalidDataException("Unable to process unknown packet type 0x" + ((int)PacketType).ToString("X2"));
            }

            this.Buffer.RemoveRange(0, Reader.Position);

            this.PreviousPacket = Packet;
            return true;
        }

        private void OnConnectionClosed(TcpRelay relay)
        {
            this.OnConnectionClosed();
        }

        private void OnConnectionClosed()
        {
            if (this.ConnectionClosed != null)
            {
                this.ConnectionClosed(this);
            }
        }
    }
}
