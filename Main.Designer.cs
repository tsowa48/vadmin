namespace vAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.классификаторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.адресаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.голосованияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кандидатыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.гражданеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.классификаторыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(918, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // классификаторыToolStripMenuItem
            // 
            this.классификаторыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.адресаToolStripMenuItem,
            this.голосованияToolStripMenuItem,
            this.кандидатыToolStripMenuItem,
            this.гражданеToolStripMenuItem});
            this.классификаторыToolStripMenuItem.Name = "классификаторыToolStripMenuItem";
            this.классификаторыToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.классификаторыToolStripMenuItem.Text = "Классификаторы";
            // 
            // адресаToolStripMenuItem
            // 
            this.адресаToolStripMenuItem.Name = "адресаToolStripMenuItem";
            this.адресаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.адресаToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.адресаToolStripMenuItem.Text = "Адреса";
            this.адресаToolStripMenuItem.Click += new System.EventHandler(this.адресаToolStripMenuItem_Click);
            // 
            // голосованияToolStripMenuItem
            // 
            this.голосованияToolStripMenuItem.Name = "голосованияToolStripMenuItem";
            this.голосованияToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.голосованияToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.голосованияToolStripMenuItem.Text = "Голосования";
            this.голосованияToolStripMenuItem.Click += new System.EventHandler(this.голосованияToolStripMenuItem_Click);
            // 
            // кандидатыToolStripMenuItem
            // 
            this.кандидатыToolStripMenuItem.Name = "кандидатыToolStripMenuItem";
            this.кандидатыToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.кандидатыToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.кандидатыToolStripMenuItem.Text = "Альтернативы";
            this.кандидатыToolStripMenuItem.Click += new System.EventHandler(this.кандидатыToolStripMenuItem_Click);
            // 
            // гражданеToolStripMenuItem
            // 
            this.гражданеToolStripMenuItem.Name = "гражданеToolStripMenuItem";
            this.гражданеToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.гражданеToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.гражданеToolStripMenuItem.Text = "Граждане";
            this.гражданеToolStripMenuItem.Click += new System.EventHandler(this.гражданеToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 692);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Администратор";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem классификаторыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem адресаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem голосованияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кандидатыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem гражданеToolStripMenuItem;
    }
}