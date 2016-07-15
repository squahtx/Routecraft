using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Routecraft
{
    public class TcpRelay
    {
        public delegate void TcpRelayEvent(TcpRelay relay);
        public delegate TcpRelayAction TcpRelayDataEvent(TcpRelay relay, byte[] data, int size);
        public event TcpRelayEvent ConnectionClosed;
        public event TcpRelayDataEvent DataReceived;

        public int Timeout { get; set; }
        public NetworkStream InboundStream { get; private set; }
        public NetworkStream OutboundStream { get; private set; }

        private bool Started = false;
        private byte[] ReadBuffer = new byte[8192];
        private DateTime LastValidRead = DateTime.Now;

        public TcpRelay(NetworkStream inboundStream, NetworkStream outboundStream)
        {
            this.Timeout = 1000;

            this.InboundStream = inboundStream;
            this.OutboundStream = outboundStream;
        }

        public void Close()
        {
            this.OnConnectionClosed();
        }

        public void Start()
        {
            if (this.Started)
            {
                return;
            }
            this.Started = true;

            this.LastValidRead = DateTime.Now;
            this.Read();
        }

        public void Write(byte[] data)
        {
            this.Write(data, data.Length);
        }

        public void Write(byte[] data, int size)
        {
            try
            {
                this.OutboundStream.Write(data, 0, size);
            }
            catch (IOException)
            {
                this.Close();
            }
            catch (ObjectDisposedException)
            {
                this.Close();
            }
        }

        private void OnConnectionClosed()
        {
            if (!this.Started)
            {
                return;
            }
            this.Started = false;
            if (this.ConnectionClosed != null)
            {
                this.ConnectionClosed(this);
            }
        }

        private void Read()
        {
            if (!this.Started)
            {
                return;
            }
            try
            {
                this.InboundStream.BeginRead(this.ReadBuffer, 0, this.ReadBuffer.Length, this.ReadEnd, null);
            }
            catch (IOException)
            {
                this.Close();
            }
        }

        private void ReadEnd(IAsyncResult result)
        {
            int BytesRead = 0;
            try
            {
                BytesRead = this.InboundStream.EndRead(result);
            }
            catch (IOException)
            {
                this.Close();
                return;
            }
            catch (ObjectDisposedException)
            {
                this.Close();
                return;
            }
            if (BytesRead == 0)
            {
                if ((DateTime.Now - this.LastValidRead).TotalMilliseconds > this.Timeout)
                {
                    this.Close();
                }
            }
            else
            {
                this.LastValidRead = DateTime.Now;

                TcpRelayAction Action = TcpRelayAction.Relay;
                if (this.DataReceived != null)
                {
                    Action = this.DataReceived(this, this.ReadBuffer, BytesRead);
                }
                if (Action == TcpRelayAction.Relay)
                {
                    this.Write(this.ReadBuffer, BytesRead);
                }
            }
            this.Read();
        }
    }
}
