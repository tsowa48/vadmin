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
        Rivals rivals;
        Peoples peoples;

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
            if (rivals == null || rivals.IsDisposed)
            {
                rivals = new Rivals();
                rivals.MdiParent = this;
                rivals.Show();
            }
            else
                rivals.Focus();
        }

        private void гражданеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (peoples == null || peoples.IsDisposed)
            {
                peoples = new Peoples();
                peoples.MdiParent = this;
                peoples.Show();
            }
            else
                peoples.Focus();
        }
    }
}
