using System;
using System.Net;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class editRival : Form
    {
        public editRival()
        {
            InitializeComponent();
        }

        long UnixTime = new DateTime(1970, 1, 1, 0, 0, 0).Ticks;

        private void editRival_Load(object sender, EventArgs e)
        {
            long now = DateTime.Now.Ticks;
            WebClient wc = new WebClient();
            String json = wc.DownloadString("http://10.0.0.100/vote");
            json = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(json));
            String[] votes = json.Split(new String[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String vote in votes)
            {
                ulong id = Convert.ToUInt64(vote.Substring(vote.IndexOf("\"id\":") + 5, vote.IndexOf(",", vote.IndexOf("\"id\":") + 5) - vote.IndexOf("\"id\":") - 5));
                String name = vote.Substring(vote.IndexOf("\"name\":") + 8, vote.IndexOf("\",", vote.IndexOf("\"name\":") + 8) - vote.IndexOf("\"name\":") - 8);
                long start = Convert.ToInt64(vote.Substring(vote.IndexOf("\"start\":") + 8, vote.IndexOf(",", vote.IndexOf("\"start\":") + 8) - vote.IndexOf("\"start\":") - 8));
                start = start * TimeSpan.TicksPerSecond + UnixTime;
                if (now < start)
                    cbVote.Items.Add(new Pair<ulong, String>(id, name + "(" + (new DateTime(start)).ToString() + ")"));
            }
            if (cbVote.Items.Count > 0)
                cbVote.SelectedIndex = 0;
            else
            {
                cbVote.Enabled = false;
                btnSave.Enabled = false;
            }
        }
    }
}
