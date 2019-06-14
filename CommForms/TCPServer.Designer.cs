namespace WindowsApplication5.CommForms
{
    partial class TCPServer
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
            this.panelServerSettings = new System.Windows.Forms.Panel();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbCommunicationEvents = new System.Windows.Forms.RichTextBox();
            this.panelServerSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelServerSettings
            // 
            this.panelServerSettings.Controls.Add(this.btnStartServer);
            this.panelServerSettings.Controls.Add(this.txtPort);
            this.panelServerSettings.Controls.Add(this.txtIpAddress);
            this.panelServerSettings.Controls.Add(this.label2);
            this.panelServerSettings.Controls.Add(this.label1);
            this.panelServerSettings.Location = new System.Drawing.Point(12, 3);
            this.panelServerSettings.Name = "panelServerSettings";
            this.panelServerSettings.Size = new System.Drawing.Size(776, 34);
            this.panelServerSettings.TabIndex = 0;
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(471, 4);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(126, 23);
            this.btnStartServer.TabIndex = 5;
            this.btnStartServer.Tag = "DisConnected";
            this.btnStartServer.Text = "Start Listening";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(283, 6);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(181, 20);
            this.txtPort.TabIndex = 3;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(54, 6);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(181, 20);
            this.txtIpAddress.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // rtbCommunicationEvents
            // 
            this.rtbCommunicationEvents.Location = new System.Drawing.Point(12, 43);
            this.rtbCommunicationEvents.Name = "rtbCommunicationEvents";
            this.rtbCommunicationEvents.Size = new System.Drawing.Size(776, 395);
            this.rtbCommunicationEvents.TabIndex = 1;
            this.rtbCommunicationEvents.Text = "";
            // 
            // TCPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbCommunicationEvents);
            this.Controls.Add(this.panelServerSettings);
            this.Name = "TCPServer";
            this.Text = "TCPServer";
            this.panelServerSettings.ResumeLayout(false);
            this.panelServerSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelServerSettings;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbCommunicationEvents;
    }
}