namespace Routecraft
{
    partial class SignModificationDialog
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.Line1 = new System.Windows.Forms.TextBox();
            this.Line2 = new System.Windows.Forms.TextBox();
            this.Line3 = new System.Windows.Forms.TextBox();
            this.Line4 = new System.Windows.Forms.TextBox();
            this.ChatPalette = new System.Windows.Forms.TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(296, 150);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(72, 28);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Line1
            // 
            this.Line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Line1.Location = new System.Drawing.Point(12, 41);
            this.Line1.Name = "Line1";
            this.Line1.Size = new System.Drawing.Size(356, 20);
            this.Line1.TabIndex = 1;
            // 
            // Line2
            // 
            this.Line2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Line2.Location = new System.Drawing.Point(12, 67);
            this.Line2.Name = "Line2";
            this.Line2.Size = new System.Drawing.Size(356, 20);
            this.Line2.TabIndex = 2;
            // 
            // Line3
            // 
            this.Line3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Line3.Location = new System.Drawing.Point(12, 93);
            this.Line3.Name = "Line3";
            this.Line3.Size = new System.Drawing.Size(356, 20);
            this.Line3.TabIndex = 3;
            // 
            // Line4
            // 
            this.Line4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Line4.Location = new System.Drawing.Point(12, 119);
            this.Line4.Name = "Line4";
            this.Line4.Size = new System.Drawing.Size(356, 20);
            this.Line4.TabIndex = 4;
            // 
            // ChatPalette
            // 
            this.ChatPalette.AcceptsTab = true;
            this.ChatPalette.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatPalette.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatPalette.Location = new System.Drawing.Point(12, 12);
            this.ChatPalette.Name = "ChatPalette";
            this.ChatPalette.Size = new System.Drawing.Size(356, 23);
            this.ChatPalette.TabIndex = 0;
            this.ChatPalette.Text = "ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜø£Ø×ƒáíóúñÑªº¿®¬½¼¡«»";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitButton.Location = new System.Drawing.Point(218, 150);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(72, 28);
            this.SubmitButton.TabIndex = 5;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // SignModificationDialog
            // 
            this.ClientSize = new System.Drawing.Size(380, 190);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.ChatPalette);
            this.Controls.Add(this.Line4);
            this.Controls.Add(this.Line3);
            this.Controls.Add(this.Line2);
            this.Controls.Add(this.Line1);
            this.Controls.Add(this.CloseButton);
            this.Name = "SignModificationDialog";
            this.Text = "Edit Sign...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox Line1;
        private System.Windows.Forms.TextBox Line2;
        private System.Windows.Forms.TextBox Line3;
        private System.Windows.Forms.TextBox Line4;
        private System.Windows.Forms.TextBox ChatPalette;
        private System.Windows.Forms.Button SubmitButton;
    }
}