namespace vAdmin
{
    partial class Addresses
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddAddressWithGPS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.кореньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(519, 605);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddAddress,
            this.mnuAddAddressWithGPS,
            this.mnuRemoveAddress,
            this.toolStripSeparator1,
            this.кореньToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(238, 98);
            // 
            // mnuAddAddress
            // 
            this.mnuAddAddress.Name = "mnuAddAddress";
            this.mnuAddAddress.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuAddAddress.Size = new System.Drawing.Size(237, 22);
            this.mnuAddAddress.Text = "Добавить";
            this.mnuAddAddress.Click += new System.EventHandler(this.mnuAddAddress_Click);
            // 
            // mnuAddAddressWithGPS
            // 
            this.mnuAddAddressWithGPS.Name = "mnuAddAddressWithGPS";
            this.mnuAddAddressWithGPS.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuAddAddressWithGPS.Size = new System.Drawing.Size(237, 22);
            this.mnuAddAddressWithGPS.Text = "Добавить с координатами";
            this.mnuAddAddressWithGPS.Click += new System.EventHandler(this.mnuAddAddressWithGPS_Click);
            // 
            // mnuRemoveAddress
            // 
            this.mnuRemoveAddress.Name = "mnuRemoveAddress";
            this.mnuRemoveAddress.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuRemoveAddress.Size = new System.Drawing.Size(237, 22);
            this.mnuRemoveAddress.Text = "Удалить";
            this.mnuRemoveAddress.Click += new System.EventHandler(this.mnuRemoveAddress_Click);
            // 
            // кореньToolStripMenuItem
            // 
            this.кореньToolStripMenuItem.Name = "кореньToolStripMenuItem";
            this.кореньToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.кореньToolStripMenuItem.Text = "Установить в корень";
            this.кореньToolStripMenuItem.Click += new System.EventHandler(this.кореньToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(234, 6);
            // 
            // Addresses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 605);
            this.Controls.Add(this.treeView1);
            this.Name = "Addresses";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Классификатор адресов";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuAddAddress;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveAddress;
        private System.Windows.Forms.ToolStripMenuItem mnuAddAddressWithGPS;
        private System.Windows.Forms.ToolStripMenuItem кореньToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

