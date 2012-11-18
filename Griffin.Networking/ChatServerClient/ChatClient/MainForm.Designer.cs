namespace ChatClient
{
    partial class MainForm
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
            this.CommandBox = new System.Windows.Forms.TextBox();
            this.ChatWindow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CommandBox
            // 
            this.CommandBox.AcceptsReturn = true;
            this.CommandBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CommandBox.Location = new System.Drawing.Point(0, 327);
            this.CommandBox.Name = "CommandBox";
            this.CommandBox.Size = new System.Drawing.Size(552, 20);
            this.CommandBox.TabIndex = 0;
            this.CommandBox.TextChanged += new System.EventHandler(this.CommandBox_TextChanged);
            this.CommandBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandBox_KeyPress);
            // 
            // ChatWindow
            // 
            this.ChatWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatWindow.Location = new System.Drawing.Point(0, 0);
            this.ChatWindow.Multiline = true;
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ChatWindow.Size = new System.Drawing.Size(552, 327);
            this.ChatWindow.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 347);
            this.Controls.Add(this.ChatWindow);
            this.Controls.Add(this.CommandBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CommandBox;
        private System.Windows.Forms.TextBox ChatWindow;
    }
}

