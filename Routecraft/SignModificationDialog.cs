using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft
{
    public partial class SignModificationDialog : Form
    {
        public delegate void SubmitEvent(UpdateSignPacket packet);
        public event SubmitEvent Submitted;

        public UpdateSignPacket Packet = new UpdateSignPacket();

        public SignModificationDialog()
        {
            this.InitializeComponent();
        }

        public SignModificationDialog(UpdateSignPacket packet)
        {
            this.InitializeComponent();

            this.Packet.X = packet.X;
            this.Packet.Y = packet.Y;
            this.Packet.Z = packet.Z;
            this.Packet.Line1 = packet.Line1;
            this.Packet.Line2 = packet.Line2;
            this.Packet.Line3 = packet.Line3;
            this.Packet.Line4 = packet.Line4;

            this.Line1.Text = this.Packet.Line1;
            this.Line2.Text = this.Packet.Line2;
            this.Line3.Text = this.Packet.Line3;
            this.Line4.Text = this.Packet.Line4;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.Packet.Line1 = this.Line1.Text;
            this.Packet.Line2 = this.Line2.Text;
            this.Packet.Line3 = this.Line3.Text;
            this.Packet.Line4 = this.Line4.Text;

            if (this.Submitted != null)
            {
                this.Submitted(this.Packet);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.SubmitButton_Click(sender, e);
            this.Close();
        }
    }
}
