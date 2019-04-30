using System;
using System.Net;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class editVote : Form
    {
        public editVote()
        {
            InitializeComponent();
        }

        private void newVote_Load(object sender, EventArgs e)
        {
            ulong treeRoot = Properties.Settings.Default.treeRoot;
            String ret = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(getAddressById(treeRoot)));
            String name = ret.Length > 2 ? ret.Substring(ret.IndexOf("\"name\":") + 8, ret.IndexOf("\"", ret.IndexOf("\"name\":") + 8) - ret.IndexOf("\"name\":") - 8) : "Все страны";
            TreeNode root = new TreeNode(name);
            root.Tag = treeRoot;
            tvAddress.Nodes.Add(root);
        }

        String getAddressById(ulong id)
        {
            WebClient wc = new WebClient();
            String ret = wc.DownloadString("http://10.0.0.100/address.php?id=" + id.ToString());
            return ret;
        }
        String getAddressByParent(ulong parent)
        {
            WebClient wc = new WebClient();
            String ret = wc.DownloadString("http://10.0.0.100/address.php?parent=" + parent.ToString());
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

        private void tvAddress_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                //TODO: отметки в дереве для родителей и потомков
                //if (e.Node.Checked == false)
                    //e.Node.Parent.Checked = false;
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                    if (node.Nodes.Count > 0)
                    {
                        TreeViewEventArgs e1 = new TreeViewEventArgs(node, e.Action);
                        tvAddress_AfterCheck(sender, e1);
                    }
                }
            }
        }
    }
}
