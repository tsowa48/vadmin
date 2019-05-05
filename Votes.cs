using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class Votes : Form
    {
        public Votes()
        {
            InitializeComponent();
        }

        long UnixTime = new DateTime(1970, 1, 1, 0, 0, 0).Ticks;

        String addVote(String name, int start, int stop, int max, string aids_array)
        {
            WebClient wc = new WebClient();
            String data = "name=" + System.Web.HttpUtility.UrlEncode(name) + "&start=" + start.ToString() + "&stop=" + stop.ToString() + "&max=" + max.ToString();
            data += aids_array;
            wc.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            String ret = System.Text.Encoding.UTF8.GetString(wc.UploadData("http://10.0.0.100/vote", System.Text.Encoding.Default.GetBytes(data)));
            return ret;
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editVote nv = new editVote();
            if (nv.ShowDialog() == DialogResult.OK)
            {
                DateTime _start = nv.dtStart.Value;
                DateTime _stop = nv.dtStop.Value;
                long startTicks = _start.Ticks - UnixTime;
                startTicks = (long)(startTicks / TimeSpan.TicksPerSecond / 100.0) * 100;
                long stopTicks = _stop.Ticks - UnixTime;
                stopTicks = (long)(stopTicks / TimeSpan.TicksPerSecond / 100.0) * 100;
                int _max = (int)nv.numMax.Value;
                String vote = addVote(nv.txtName.Text, (int)startTicks, (int)stopTicks, _max, updateAddress(nv.tvAddress));//<--------------------------

                ulong id = Convert.ToUInt64(vote.Substring(vote.IndexOf("\"id\":") + 5, vote.IndexOf(",", vote.IndexOf("\"id\":") + 5) - vote.IndexOf("\"id\":") - 5));
                String name = vote.Substring(vote.IndexOf("\"name\":") + 8, vote.IndexOf("\",", vote.IndexOf("\"name\":") + 8) - vote.IndexOf("\"name\":") - 8);
                long start = Convert.ToInt64(vote.Substring(vote.IndexOf("\"start\":") + 8, vote.IndexOf(",", vote.IndexOf("\"start\":") + 8) - vote.IndexOf("\"start\":") - 8));
                long stop = Convert.ToInt64(vote.Substring(vote.IndexOf("\"stop\":") + 7, vote.IndexOf(",", vote.IndexOf("\"stop\":") + 7) - vote.IndexOf("\"stop\":") - 7));
                long max = Convert.ToInt64(vote.Substring(vote.IndexOf("\"max\":") + 6, vote.IndexOf("}", vote.IndexOf("\"max\":") + 6) - vote.IndexOf("\"max\":") - 6));

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = id;
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = name });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = new DateTime(start * TimeSpan.TicksPerSecond + UnixTime).ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = new DateTime(stop * TimeSpan.TicksPerSecond + UnixTime).ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = max });

                dataGridView1.Rows.Add(row);
            }
        }

        private void Votes_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            String json = wc.DownloadString("http://10.0.0.100/vote");
            json = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(json));
            String[] votes = json.Split(new String[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach(String vote in votes)
            {
                ulong id = Convert.ToUInt64(vote.Substring(vote.IndexOf("\"id\":") + 5, vote.IndexOf(",", vote.IndexOf("\"id\":") + 5) - vote.IndexOf("\"id\":") - 5));
                String name = vote.Substring(vote.IndexOf("\"name\":") + 8, vote.IndexOf("\",", vote.IndexOf("\"name\":") + 8) - vote.IndexOf("\"name\":") - 8);
                long start = Convert.ToInt64(vote.Substring(vote.IndexOf("\"start\":") + 8, vote.IndexOf(",", vote.IndexOf("\"start\":") + 8) - vote.IndexOf("\"start\":") - 8));
                long stop = Convert.ToInt64(vote.Substring(vote.IndexOf("\"stop\":") + 7, vote.IndexOf(",", vote.IndexOf("\"stop\":") + 7) - vote.IndexOf("\"stop\":") - 7));
                long max = Convert.ToInt64(vote.Substring(vote.IndexOf("\"max\":") + 6).Replace("}", "").Replace("]", ""));

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = id;
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = name });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = new DateTime(start * TimeSpan.TicksPerSecond + UnixTime).ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = new DateTime(stop * TimeSpan.TicksPerSecond + UnixTime).ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = max });

                dataGridView1.Rows.Add(row);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WebClient wc = new WebClient();
            ulong id = Convert.ToUInt64(dataGridView1.SelectedRows[0].Tag);
            String vote = wc.DownloadString("http://10.0.0.100/vote?id=" + id.ToString());
            vote = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(vote));
            String name = vote.Substring(vote.IndexOf("\"name\":") + 8, vote.IndexOf("\",", vote.IndexOf("\"name\":") + 8) - vote.IndexOf("\"name\":") - 8);
            long start = Convert.ToInt64(vote.Substring(vote.IndexOf("\"start\":") + 8, vote.IndexOf(",", vote.IndexOf("\"start\":") + 8) - vote.IndexOf("\"start\":") - 8));
            long stop = Convert.ToInt64(vote.Substring(vote.IndexOf("\"stop\":") + 7, vote.IndexOf(",", vote.IndexOf("\"stop\":") + 7) - vote.IndexOf("\"stop\":") - 7));
            long max = Convert.ToInt64(vote.Substring(vote.IndexOf("\"max\":") + 6, vote.IndexOf("}", vote.IndexOf("\"max\":") + 6) - vote.IndexOf("\"max\":") - 6));

            editVote nv = new editVote();
            nv.txtName.Text = name;
            nv.dtStart.Value = new DateTime(start * TimeSpan.TicksPerSecond + UnixTime);
            nv.dtStop.Value = new DateTime(stop * TimeSpan.TicksPerSecond + UnixTime);
            nv.numMax.Value = max;
            if (nv.ShowDialog() == DialogResult.OK)
            {
                String _name = nv.txtName.Text;
                DateTime _start = nv.dtStart.Value;
                DateTime _stop = nv.dtStop.Value;
                long startTicks = _start.Ticks - UnixTime;
                startTicks = (long)(startTicks / TimeSpan.TicksPerSecond / 100.0) * 100;
                long stopTicks = _stop.Ticks - UnixTime;
                stopTicks = (long)(stopTicks / TimeSpan.TicksPerSecond / 100.0) * 100;
                int _max = (int)nv.numMax.Value;

                bool isUpdated = false;
                try
                {
                    String data = "id=" + id.ToString() + "&name=" + System.Web.HttpUtility.UrlEncode(_name) +
                                  "&start=" + startTicks.ToString() + "&stop=" + stopTicks + "&max=" + _max;
                    data += updateAddress(nv.tvAddress);//<----------------------------------------------------------------------------
                    WebRequest r = WebRequest.Create("http://10.0.0.100/vote");
                    r.ContentType = "application/x-www-form-urlencoded";
                    r.Method = "PUT";
                    r.GetRequestStream().Write(System.Text.Encoding.Default.GetBytes(data), 0, data.Length);
                    if ((int)((HttpWebResponse)r.GetResponse()).StatusCode == 423)
                        isUpdated = false;
                    else if (((HttpWebResponse)r.GetResponse()).StatusCode == HttpStatusCode.OK)
                        isUpdated = true;
                }
                catch (WebException wex)
                {
                    if ((int)((HttpWebResponse)wex.Response).StatusCode == 423)
                        isUpdated = false;
                }
                if (isUpdated)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = id;
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = _name });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = new DateTime(startTicks * TimeSpan.TicksPerSecond + UnixTime).ToString() });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = new DateTime(stopTicks * TimeSpan.TicksPerSecond + UnixTime).ToString() });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = _max });

                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    dataGridView1.Rows.Add(row);
                }
                else
                {
                    MessageBox.Show("Изменение параметров голосования запрещено", "Голосование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private String updateAddress(TreeView tv)
        {
            List<ulong> aids = new List<ulong>();
            CheckedNodes(aids, tv.Nodes[0]);
            return "";//return "&aids=" + String.Join(",", aids);
        }

        void CheckedNodes(List<ulong> aids, TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Checked)
                    aids.Add(Convert.ToUInt64(n.Tag));
                CheckedNodes(aids, n);
            }
        }
    }
}
