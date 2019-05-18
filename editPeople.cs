using System;
using System.Net;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class editPeople : Form
    {
        public editPeople()
        {
            InitializeComponent();
        }

        private void editPeople_Load(object sender, EventArgs e)
        {
            ulong treeRoot = 0;
            String ret = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(getAddressById(treeRoot)));
            String name = ret.Length > 2 ? ret.Substring(ret.IndexOf("\"name\":") + 8, ret.IndexOf("\"", ret.IndexOf("\"name\":") + 8) - ret.IndexOf("\"name\":") - 8) : "Все страны";
            TreeNode root = new TreeNode(name);
            root.Tag = treeRoot;
            tvAddress.Nodes.Add(root);
        }

        String getAddressById(ulong id)
        {
            WebClient wc = new WebClient();
            String ret = wc.DownloadString(Program.BASE + "/address?id=" + id.ToString());
            return ret;
        }
        String getAddressByParent(ulong parent)
        {
            WebClient wc = new WebClient();
            String ret = wc.DownloadString(Program.BASE + "/address?parent=" + parent.ToString());
            return ret;
        }

        private void tvAddress_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tn = e.Node;
            String retFull = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(getAddressByParent(Convert.ToUInt64(tn.Tag))));
            if (retFull.Length > 2)
            {
                tn.Nodes.Clear();
                String[] _ret = retFull.Split('\n');
                foreach (String ret in _ret)
                {
                    String name = ret.Substring(ret.IndexOf("\"name\":") + 8, ret.IndexOf("\"", ret.IndexOf("\"name\":") + 8) - ret.IndexOf("\"name\":") - 8);
                    String id = ret.Substring(ret.IndexOf("\"id\":") + 5, ret.IndexOf(",", ret.IndexOf("\"id\":") + 5) - ret.IndexOf("\"id\":") - 5);
                    TreeNode newTn = new TreeNode(name);
                    newTn.Tag = id;
                    tn.Nodes.Add(newTn);
                    tn.Expand();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtFio.Text != "" && cbSex.SelectedIndex > -1 && tvAddress.SelectedNode != null)
                btnOk.Enabled = true;
        }
    }
}
