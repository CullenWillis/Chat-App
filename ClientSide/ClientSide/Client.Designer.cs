namespace ClientSide
{
    partial class Client
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
            this.textboxMessage = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.textboxLog = new System.Windows.Forms.RichTextBox();
            this.checkboxImportant = new System.Windows.Forms.CheckBox();
            this.listboxUsers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkboxTimeStamps = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textboxMessage
            // 
            this.textboxMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(34)))));
            this.textboxMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxMessage.ForeColor = System.Drawing.Color.White;
            this.textboxMessage.Location = new System.Drawing.Point(12, 318);
            this.textboxMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxMessage.MaxLength = 255;
            this.textboxMessage.Multiline = true;
            this.textboxMessage.Name = "textboxMessage";
            this.textboxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textboxMessage.ShortcutsEnabled = false;
            this.textboxMessage.Size = new System.Drawing.Size(601, 74);
            this.textboxMessage.TabIndex = 1;
            this.textboxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textboxMessage_KeyDown);
            this.textboxMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textboxMessage_KeyUp);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.buttonDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDisconnect.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisconnect.ForeColor = System.Drawing.Color.White;
            this.buttonDisconnect.Location = new System.Drawing.Point(708, 405);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(120, 31);
            this.buttonDisconnect.TabIndex = 4;
            this.buttonDisconnect.Text = "DISCONNECT";
            this.buttonDisconnect.UseVisualStyleBackColor = false;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textboxLog
            // 
            this.textboxLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.textboxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxLog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxLog.ForeColor = System.Drawing.Color.White;
            this.textboxLog.Location = new System.Drawing.Point(12, 30);
            this.textboxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.Size = new System.Drawing.Size(603, 283);
            this.textboxLog.TabIndex = 14;
            this.textboxLog.Text = "";
            // 
            // checkboxImportant
            // 
            this.checkboxImportant.BackColor = System.Drawing.Color.Transparent;
            this.checkboxImportant.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxImportant.ForeColor = System.Drawing.Color.White;
            this.checkboxImportant.Location = new System.Drawing.Point(12, 405);
            this.checkboxImportant.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkboxImportant.Name = "checkboxImportant";
            this.checkboxImportant.Size = new System.Drawing.Size(225, 25);
            this.checkboxImportant.TabIndex = 0;
            this.checkboxImportant.Text = "FLAG AS IMPORTANT";
            this.checkboxImportant.UseVisualStyleBackColor = false;
            this.checkboxImportant.CheckedChanged += new System.EventHandler(this.checkboxImportant_CheckedChanged);
            // 
            // listboxUsers
            // 
            this.listboxUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.listboxUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxUsers.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxUsers.ForeColor = System.Drawing.Color.Lime;
            this.listboxUsers.FormattingEnabled = true;
            this.listboxUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listboxUsers.ItemHeight = 18;
            this.listboxUsers.Location = new System.Drawing.Point(621, 30);
            this.listboxUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listboxUsers.Name = "listboxUsers";
            this.listboxUsers.Size = new System.Drawing.Size(207, 362);
            this.listboxUsers.TabIndex = 0;
            this.listboxUsers.DoubleClick += new System.EventHandler(this.listboxUsers_DoubleClick);
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(620, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "ONLINE USERS";
            // 
            // checkboxTimeStamps
            // 
            this.checkboxTimeStamps.AutoSize = true;
            this.checkboxTimeStamps.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxTimeStamps.ForeColor = System.Drawing.Color.White;
            this.checkboxTimeStamps.Location = new System.Drawing.Point(12, 436);
            this.checkboxTimeStamps.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxTimeStamps.Name = "checkboxTimeStamps";
            this.checkboxTimeStamps.Size = new System.Drawing.Size(132, 21);
            this.checkboxTimeStamps.TabIndex = 16;
            this.checkboxTimeStamps.Text = "TIME STAMPS";
            this.checkboxTimeStamps.UseVisualStyleBackColor = true;
            this.checkboxTimeStamps.CheckedChanged += new System.EventHandler(this.checkboxTimeStamps_CheckedChanged);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(845, 467);
            this.Controls.Add(this.checkboxTimeStamps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listboxUsers);
            this.Controls.Add(this.checkboxImportant);
            this.Controls.Add(this.textboxLog);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.textboxMessage);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(863, 514);
            this.MinimumSize = new System.Drawing.Size(863, 514);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D-Messenger Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textboxMessage;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.RichTextBox textboxLog;
        private System.Windows.Forms.CheckBox checkboxImportant;
        private System.Windows.Forms.ListBox listboxUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkboxTimeStamps;
    }
}