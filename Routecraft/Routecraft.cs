using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Routecraft.Minecraft.Storage;
using Routecraft.Minecraft;
using Routecraft.Minecraft.Networking;
using Routecraft.Minecraft.Networking.Packets;
using Routecraft.Minecraft.PacketHandlers.MapChunk;

namespace Routecraft
{
    public partial class Main : Form
    {
        private Relay Relay = null;
        private Thread AcceptingThread;

        private MinecraftDatabase MinecraftDatabase;

        private TcpListener Listener = new TcpListener(IPAddress.Loopback, 25565);

        public Main()
        {
            this.InitializeComponent();

            this.MinecraftDatabase = new MinecraftDatabase();

            this.AcceptingThread = new Thread(this.Thread_AcceptClients);
            this.AcceptingThread.Start();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Listener.Stop();
            this.AcceptingThread.Abort();
        }

        private void Thread_AcceptClients()
        {
            this.Listener.Start();
            while (true)
            {
                TcpClient Client = this.Listener.AcceptTcpClient();
                string Address = "localhost";
                ushort Port = 27031;

                Address = "92.239.8.47";
                Port = 25565;

                Address = "131.111.179.83";
                Port = 25569;

                // Address = "localhost";
                // Port = 27031;

                Relay Relay = new Relay(this.MinecraftDatabase, Address, Port);
                Relay.Start(Client);
                if (this.Relay != null)
                {
                    this.UnhookRelay();
                }
                this.Relay = Relay;
                this.HookRelay();
            }
        }

        private void HookRelay()
        {
            this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.Chat, this.Packet_Chat);
            // this.Relay.ServerOutboundRelay.AddPacketHandler(PacketType.MapChunk, new XRayPacketHandler());
            this.Relay.ServerInboundRelay.AddPacketHandler(PacketType.UpdateSign, this.Packet_UpdateSign);

            new WorldBackupCreator().Initialize(this.Relay);
        }

        private void UnhookRelay()
        {
            this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.Chat, this.Packet_Chat);
            // this.Relay.ServerOutboundRelay.RemovePacketHandler(PacketType.MapChunk);
            this.Relay.ServerInboundRelay.RemovePacketHandler(PacketType.UpdateSign, this.Packet_UpdateSign);
        }

        private MinecraftRelayAction Packet_Chat(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            ChatPacket Packet = (ChatPacket)packet;
            this.Invoke(new MethodInvoker(
                delegate()
                {
                    ListViewItem Item = this.Chat.Items.Add(DateTime.Now.ToLongTimeString());
                    Item.UseItemStyleForSubItems = false;
                    string Message = Packet.Message;
                    Color Color = Color.Black;
                    if (Message.Length >= 2)
                    {
                        if (Message[0] == '§')
                        {
                            Color = TextColors.GetForegroundColor(Message[1]);
                            Message = Message.Substring(2);
                        }
                    }
                    ListViewItem.ListViewSubItem SubItem = Item.SubItems.Add(Message);
                    SubItem.ForeColor = Color;
                    Item.EnsureVisible();
                }
            ));

            return MinecraftRelayAction.Relay;
        }

        private MinecraftRelayAction Packet_UpdateSign(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            UpdateSignPacket Packet = (UpdateSignPacket)packet;
            if (!this.AllowSignModification.Checked)
            {
                return MinecraftRelayAction.Relay;
            }

            this.Invoke(new MethodInvoker(
                delegate()
                {
                    SignModificationDialog Dialog = new SignModificationDialog(Packet);
                    Dialog.Show();
                    Dialog.Submitted += delegate(UpdateSignPacket newPacket)
                    {
                        if (this.Relay == null) { return; }
                        this.Relay.ServerInboundRelay.QueuePacket(PacketType.UpdateSign, newPacket);
                    };
                }
            ));
            return MinecraftRelayAction.Drop;
        }

        private void Chat_SizeChanged(object sender, EventArgs e)
        {
            this.Chat.Columns[1].Width = -2;
        }

        private void ChatSend_Click(object sender, EventArgs e)
        {
            this.SendChat();
        }

        private void SendChat()
        {
            if (this.ChatBox.Text == "")
            {
                return;
            }
            ChatPacket Packet = new ChatPacket();
            Packet.Message = this.ChatBox.Text;

            if (this.Relay != null)
            {
                this.Relay.ServerInboundRelay.QueuePacket(PacketType.Chat, Packet);
            }

            this.ChatBox.Text = "";
        }

        private void ChatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SendChat();
                e.Handled = true;
            }
        }
    }
}
