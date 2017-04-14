namespace ServerSide
{
    partial class Server
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
            this.textboxLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textboxLog
            // 
            this.textboxLog.Location = new System.Drawing.Point(12, 12);
            this.textboxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxLog.Multiline = true;
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.Size = new System.Drawing.Size(600, 299);
            this.textboxLog.TabIndex = 1;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 324);
            this.Controls.Add(this.textboxLog);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(644, 371);
            this.MinimumSize = new System.Drawing.Size(644, 371);
            this.Name = "Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D-Messenger Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxLog;
    }
}