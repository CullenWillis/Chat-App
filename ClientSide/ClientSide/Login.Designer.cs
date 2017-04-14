namespace ClientSide
{
    partial class Login
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
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.textboxIPAddress = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textboxUsername
            // 
            this.textboxUsername.Location = new System.Drawing.Point(95, 95);
            this.textboxUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(200, 22);
            this.textboxUsername.TabIndex = 3;
            this.textboxUsername.Tag = "";
            // 
            // textboxIPAddress
            // 
            this.textboxIPAddress.Location = new System.Drawing.Point(95, 12);
            this.textboxIPAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxIPAddress.Name = "textboxIPAddress";
            this.textboxIPAddress.Size = new System.Drawing.Size(200, 22);
            this.textboxIPAddress.TabIndex = 1;
            this.textboxIPAddress.Tag = "";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(196, 124);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 25);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Tag = "";
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Server IP";
            // 
            // textboxPort
            // 
            this.textboxPort.Location = new System.Drawing.Point(196, 42);
            this.textboxPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxPort.Name = "textboxPort";
            this.textboxPort.Size = new System.Drawing.Size(97, 22);
            this.textboxPort.TabIndex = 2;
            this.textboxPort.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Port";
            // 
            // Login
            // 
            this.AcceptButton = this.buttonConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 153);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textboxIPAddress);
            this.Controls.Add(this.textboxUsername);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(325, 200);
            this.MinimumSize = new System.Drawing.Size(325, 200);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D-Messenger Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.TextBox textboxIPAddress;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxPort;
        private System.Windows.Forms.Label label3;
    }
}