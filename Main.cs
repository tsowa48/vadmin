using System;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        Addresses address;
        Votes votes;
        Contenders contenders;

        private void адресаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (address == null || address.IsDisposed)
            {
                address = new Addresses();
                address.MdiParent = this;
                address.Show();
            }
            else
                address.Focus();
        }

        private void голосованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (votes == null || votes.IsDisposed)
            {
                votes = new Votes();
                votes.MdiParent = this;
                votes.Show();
            }
            else
                votes.Focus();
        }

        private void кандидатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contenders == null || contenders.IsDisposed)
            {
                contenders = new Contenders();
                contenders.MdiParent = this;
                contenders.Show();
            }
            else
                contenders.Focus();
        }
    }
}
