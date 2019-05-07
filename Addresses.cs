using System;
using System.Net;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class Addresses : Form
    {
        public Addresses()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ulong treeRoot = Properties.Settings.Default.treeRoot;
            String ret = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(getAddressById(treeRoot)));
            String name = ret.Length > 2 ? ret.Substring(ret.IndexOf("\"name\":") + 8, ret.IndexOf("\"", ret.IndexOf("\"name\":") + 8) - ret.IndexOf("\"name\":") - 8) : "Все страны";
            TreeNode root = new TreeNode(name);
            root.Tag = treeRoot;
            treeView1.Nodes.Add(root);
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
        String addAddress(String name, ulong parent, String fullAddress)
        {
            WebClient wc1 = new WebClient();
            String pos = wc1.DownloadString("https://geocode-maps.yandex.ru/1.x/?geocode=" + System.Web.HttpUtility.UrlEncode(fullAddress) + "&kind=house&format=json");
            pos = pos.Substring(pos.IndexOf("\"Point\":{\"pos\":\"") + 16, pos.IndexOf("\"",
                pos.IndexOf("\"Point\":{\"pos\":\"") + 16) - pos.IndexOf("\"Point\":{\"pos\":\"") - 16);
            WebClient wc = new WebClient();
            String data = "name=" + System.Web.HttpUtility.UrlEncode(name) + "&parent=" + parent.ToString() + "&lon=" + pos.Split(' ')[0] + "&lat=" + pos.Split(' ')[1];
            wc.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            String ret = System.Text.Encoding.Default.GetString(wc.UploadData(Program.BASE + "/address", System.Text.Encoding.Default.GetBytes(data)));
            return ret;
        }
        String addAddress(String name, ulong parent)
        {
            WebClient wc = new WebClient();
            String data = "name=" + System.Web.HttpUtility.UrlEncode(name) + "&parent=" + parent.ToString();
            wc.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            String ret = System.Text.Encoding.Default.GetString(wc.UploadData(Program.BASE + "/address", System.Text.Encoding.Default.GetBytes(data)));
            return ret;
        }
        String addAddress(String name)
        {
            WebClient wc = new WebClient();
            String data = "name=" + System.Web.HttpUtility.UrlEncode(name) + "&parent=0";
            wc.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            String ret = System.Text.Encoding.Default.GetString(wc.UploadData(Program.BASE + "/address", System.Text.Encoding.Default.GetBytes(data)));
            return ret;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
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

        private void mnuAddAddress_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            String name = Microsoft.VisualBasic.Interaction.InputBox("Новый адрес:");
            UInt64 parent = Convert.ToUInt64(tn.Tag);
            String json = addAddress(name, parent);
            TreeNode newTn = new TreeNode(name);
            newTn.Tag = json.Substring(json.IndexOf("\"id\":") + 5, json.IndexOf(",") - json.IndexOf("\"id\":") - 5);
            tn.Nodes.Add(newTn);
        }

        private void mnuAddAddressWithGPS_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            String address = tn.FullPath.Substring(tn.FullPath.IndexOf("\\") + 1).Replace("\\", ", ");
            String name = Microsoft.VisualBasic.Interaction.InputBox("Новый адрес:");
            UInt64 parent = Convert.ToUInt64(tn.Tag);
            String id = addAddress(name, parent, address + ", " + name);
            TreeNode newTn = new TreeNode(name);
            newTn.Tag = id.Substring(id.IndexOf("\"id\":") + 5, id.IndexOf("}") - id.IndexOf("\"id\":") - 5);
            tn.Nodes.Add(newTn);
            //UInt64 id = Convert.ToUInt64(tn.Tag);
            //WebClient wc1 = new WebClient();
            //String address = tn.FullPath.Substring(tn.FullPath.IndexOf("\\") + 1).Replace("\\", ", ");
            //String pos = wc1.DownloadString("https://geocode-maps.yandex.ru/1.x/?geocode=" + System.Web.HttpUtility.UrlEncode(address) + "&kind=house&format=json");
            //pos = pos.Substring(pos.IndexOf("\"Point\":{\"pos\":\"") + 16, pos.IndexOf("\"",
            //pos.IndexOf("\"Point\":{\"pos\":\"") + 16) - pos.IndexOf("\"Point\":{\"pos\":\"") - 16);

            //String data = "id=" + id.ToString() + "&lon=" + pos.Split(' ')[0] + "&lat=" + pos.Split(' ')[1];
            //WebRequest r = WebRequest.Create(Program.BASE + "/address");
            //r.ContentType = "application/x-www-form-urlencoded";
            //r.Method = "PUT";
            //r.GetRequestStream().Write(System.Text.Encoding.Default.GetBytes(data), 0, data.Length);

            //StreamReader sr = new StreamReader(r.GetResponse().GetResponseStream());
            //sr.ReadToEnd();
            //sr.Close();
        }

        private void mnuRemoveAddress_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            UInt64 id = Convert.ToUInt64(tn.Tag);
            String data = "id=" + id.ToString();
            try
            {
                WebRequest r = WebRequest.Create(Program.BASE + "/address");
                r.ContentType = "application/x-www-form-urlencoded";
                r.Method = "DELETE";
                r.GetRequestStream().Write(System.Text.Encoding.Default.GetBytes(data), 0, data.Length);
                if (((HttpWebResponse)r.GetResponse()).StatusCode == HttpStatusCode.NotFound)
                    tn.Remove();
            }
            catch (WebException wex)
            {
                if(((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                    tn.Remove();
            }
        }

        private void кореньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ulong id = Convert.ToUInt64(treeView1.SelectedNode.Tag);
            Properties.Settings.Default.treeRoot = id;
            Properties.Settings.Default.Save();
        }
    }
}
