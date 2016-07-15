using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;
using Routecraft.Minecraft;
using Routecraft.Minecraft.Networking;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft
{
    public class Relay
    {
        public delegate void RelayEvent(Relay relay);
        public event RelayEvent RelayClosed;

        public MinecraftDatabase MinecraftDatabase { get; private set; }

        private TcpClient ClientToLocal = null;
        private TcpClient LocalToServer = null;
        private NetworkStream ClientToLocalStream = null;
        private NetworkStream LocalToServerStream = null;
        private bool Closed = false;

        public MinecraftRelay ServerInboundRelay { get; private set; }
        public MinecraftRelay ServerOutboundRelay { get; private set; }

        public string Address   = "92.239.8.47";
        public ushort Port      = 25565;

        public Relay(MinecraftDatabase minecraftDatabase)
        {
            this.MinecraftDatabase = minecraftDatabase;
        }

        public Relay(MinecraftDatabase minecraftDatabase, string address)
            : this(minecraftDatabase, address, 25565)
        { }

        public Relay(MinecraftDatabase minecraftDatabase, string address, ushort port)
        {
            this.MinecraftDatabase = minecraftDatabase;
            this.Address = address;
            this.Port = port;
        }

        public bool Start(TcpClient client)
        {
            this.ClientToLocal = client;
            this.LocalToServer = new TcpClient();
            try
            {
                this.LocalToServer.Connect(this.Address, this.Port);
            }
            catch (SocketException)
            {
                this.Close("unable to connect to real server");
                return false;
            }

            this.ClientToLocalStream = this.ClientToLocal.GetStream();
            this.LocalToServerStream = this.LocalToServer.GetStream();

            this.ServerInboundRelay = new MinecraftRelay(this.MinecraftDatabase, new TcpRelay(this.ClientToLocalStream, this.LocalToServerStream), new ClientPacketMap());
            this.ServerOutboundRelay = new MinecraftRelay(this.MinecraftDatabase, new TcpRelay(this.LocalToServerStream, this.ClientToLocalStream), new ServerPacketMap());

            this.ServerInboundRelay.ConnectionClosed += this.OnConnectionClosed;
            this.ServerOutboundRelay.ConnectionClosed += this.OnConnectionClosed;

            this.ServerInboundRelay.Start();
            this.ServerOutboundRelay.Start();

            return true;
        }

        private void Close()
        {
            this.Close(null);
        }

        private void Close(string reason)
        {
            if (this.Closed)
            {
                return;
            }

            if (reason == null)
            {
                Debug.WriteLine("Relay closed.");
            }
            else
            {
                Debug.WriteLine("Relay closed (" + reason + ").");
            }

            this.Closed = true;

            if (this.RelayClosed != null)
            {
                this.RelayClosed(this);
            }

            if (this.ServerInboundRelay != null) { this.ServerInboundRelay.Close(); }
            if (this.ServerOutboundRelay != null) { this.ServerOutboundRelay.Close(); }

            if (this.ClientToLocalStream != null) { this.ClientToLocalStream.Close(); }
            if (this.LocalToServerStream != null) { this.LocalToServerStream.Close(); }
            if (this.ClientToLocal != null) { this.ClientToLocal.Close(); }
            if (this.LocalToServer != null) { this.LocalToServer.Close(); }

            this.ClientToLocalStream = null;
            this.LocalToServerStream = null;
            this.ClientToLocal = null;
            this.LocalToServer = null;
        }

        private void OnConnectionClosed(MinecraftRelay relay)
        {
            this.Close();
        }

        private void OnConnectionClosed(TcpRelay relay)
        {
            this.Close();
        }
    }
}