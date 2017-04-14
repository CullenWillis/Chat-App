namespace ServerSide
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
            this.textboxIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxUsers = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textboxIPAddress
            // 
            this.textboxIPAddress.Location = new System.Drawing.Point(70, 10);
            this.textboxIPAddress.Margin = new System.Windows.Forms.Padding(2);
            this.textboxIPAddress.Name = "textboxIPAddress";
            this.textboxIPAddress.Size = new System.Drawing.Size(151, 20);
            this.textboxIPAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "IP Address";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(146, 105);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 20);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Port";
            // 
            // textboxPort
            // 
            this.textboxPort.Location = new System.Drawing.Point(146, 34);
            this.textboxPort.Margin = new System.Windows.Forms.Padding(2);
            this.textboxPort.Name = "textboxPort";
            this.textboxPort.Size = new System.Drawing.Size(75, 20);
            this.textboxPort.TabIndex = 2;
            this.textboxPort.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Number of Users";
            // 
            // textboxUsers
            // 
            this.textboxUsers.Location = new System.Drawing.Point(146, 81);
            this.textboxUsers.Margin = new System.Windows.Forms.Padding(2);
            this.textboxUsers.Name = "textboxUsers";
            this.textboxUsers.Size = new System.Drawing.Size(75, 20);
            this.textboxUsers.TabIndex = 3;
            this.textboxUsers.Text = "2";
            // 
            // Login
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 136);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textboxPort);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxIPAddress);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(248, 175);
            this.MinimumSize = new System.Drawing.Size(248, 175);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D-Messenger Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxUsers;
    }
}