namespace ChessGame
{
    partial class StartForm
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
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnHost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(12, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(341, 20);
            this.txtIp.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(359, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(108, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnHost
            // 
            this.btnHost.Location = new System.Drawing.Point(12, 38);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(455, 37);
            this.btnHost.TabIndex = 2;
            this.btnHost.Text = "Host Game";
            this.btnHost.UseVisualStyleBackColor = true;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 87);
            this.Controls.Add(this.btnHost);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtIp);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnHost;
    }
}