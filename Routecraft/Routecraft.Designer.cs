namespace Routecraft
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ServerBox = new System.Windows.Forms.GroupBox();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.ChatSend = new System.Windows.Forms.Button();
            this.ChatPalette = new System.Windows.Forms.TextBox();
            this.AllowSignModification = new System.Windows.Forms.CheckBox();
            this.Chat = new Routecraft.ExtendedListView();
            this.ChatTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.ChatMessageColumn = new System.Windows.Forms.ColumnHeader();
            this.ServerBox.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerBox
            // 
            this.ServerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerBox.Controls.Add(this.ServerTextBox);
            this.ServerBox.Controls.Add(this.ServerLabel);
            this.ServerBox.Location = new System.Drawing.Point(12, 27);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(551, 49);
            this.ServerBox.TabIndex = 0;
            this.ServerBox.TabStop = false;
            this.ServerBox.Text = "Server";
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerTextBox.Location = new System.Drawing.Point(53, 19);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(492, 20);
            this.ServerTextBox.TabIndex = 1;
            this.ServerTextBox.Text = "92.239.8.47";
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(6, 22);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(41, 13);
            this.ServerLabel.TabIndex = 0;
            this.ServerLabel.Text = "Server:";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(575, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(89, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.ExitMenuItem.Text = "E&xit";
            // 
            // TrayIcon
            // 
            this.TrayIcon.Text = "Routecraft";
            // 
            // ChatBox
            // 
            this.ChatBox.AcceptsTab = true;
            this.ChatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatBox.Location = new System.Drawing.Point(12, 341);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Size = new System.Drawing.Size(481, 20);
            this.ChatBox.TabIndex = 3;
            this.ChatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatBox_KeyDown);
            // 
            // ChatSend
            // 
            this.ChatSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatSend.Location = new System.Drawing.Point(499, 336);
            this.ChatSend.Name = "ChatSend";
            this.ChatSend.Size = new System.Drawing.Size(64, 28);
            this.ChatSend.TabIndex = 4;
            this.ChatSend.Text = "Send";
            this.ChatSend.UseVisualStyleBackColor = true;
            this.ChatSend.Click += new System.EventHandler(this.ChatSend_Click);
            // 
            // ChatPalette
            // 
            this.ChatPalette.AcceptsTab = true;
            this.ChatPalette.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatPalette.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatPalette.Location = new System.Drawing.Point(12, 307);
            this.ChatPalette.Name = "ChatPalette";
            this.ChatPalette.Size = new System.Drawing.Size(551, 23);
            this.ChatPalette.TabIndex = 5;
            this.ChatPalette.Text = "ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜø£Ø×ƒáíóúñÑªº¿®¬½¼¡«»";
            // 
            // AllowSignModification
            // 
            this.AllowSignModification.AutoSize = true;
            this.AllowSignModification.Location = new System.Drawing.Point(12, 82);
            this.AllowSignModification.Name = "AllowSignModification";
            this.AllowSignModification.Size = new System.Drawing.Size(135, 17);
            this.AllowSignModification.TabIndex = 6;
            this.AllowSignModification.Text = "Allow Sign Modification";
            this.AllowSignModification.UseVisualStyleBackColor = true;
            // 
            // Chat
            // 
            this.Chat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Chat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChatTimeColumn,
            this.ChatMessageColumn});
            this.Chat.FullRowSelect = true;
            this.Chat.GridLines = true;
            this.Chat.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Chat.Location = new System.Drawing.Point(12, 103);
            this.Chat.MultiSelect = false;
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(551, 198);
            this.Chat.TabIndex = 2;
            this.Chat.UseCompatibleStateImageBehavior = false;
            this.Chat.View = System.Windows.Forms.View.Details;
            this.Chat.SizeChanged += new System.EventHandler(this.Chat_SizeChanged);
            // 
            // ChatTimeColumn
            // 
            this.ChatTimeColumn.Text = "Time";
            this.ChatTimeColumn.Width = 79;
            // 
            // ChatMessageColumn
            // 
            this.ChatMessageColumn.Text = "Message";
            this.ChatMessageColumn.Width = 468;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 376);
            this.Controls.Add(this.AllowSignModification);
            this.Controls.Add(this.ChatPalette);
            this.Controls.Add(this.ChatSend);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Main";
            this.Text = "Routecraft";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.ServerBox.ResumeLayout(false);
            this.ServerBox.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ServerBox;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.Label ServerLabel;
        private Routecraft.ExtendedListView Chat;
        private System.Windows.Forms.ColumnHeader ChatTimeColumn;
        private System.Windows.Forms.ColumnHeader ChatMessageColumn;
        private System.Windows.Forms.TextBox ChatBox;
        private System.Windows.Forms.Button ChatSend;
        private System.Windows.Forms.TextBox ChatPalette;
        private System.Windows.Forms.CheckBox AllowSignModification;
    }
}

