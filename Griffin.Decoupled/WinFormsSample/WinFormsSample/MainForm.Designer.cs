namespace WinFormsSample
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
            this.CboNote = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.Note = new System.Windows.Forms.TextBox();
            this.btnComplete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CboNote
            // 
            this.CboNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNote.FormattingEnabled = true;
            this.CboNote.Location = new System.Drawing.Point(23, 13);
            this.CboNote.Name = "CboNote";
            this.CboNote.Size = new System.Drawing.Size(466, 24);
            this.CboNote.TabIndex = 0;
            this.CboNote.SelectedIndexChanged += new System.EventHandler(this.CboNote_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(253, 322);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // Note
            // 
            this.Note.Location = new System.Drawing.Point(23, 57);
            this.Note.Multiline = true;
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(466, 243);
            this.Note.TabIndex = 2;
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(334, 322);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(155, 23);
            this.btnComplete.TabIndex = 3;
            this.btnComplete.Text = "Mark as completed";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 387);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.CboNote);
            this.Name = "MainForm";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboNote;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox Note;
        private System.Windows.Forms.Button btnComplete;
    }
}

