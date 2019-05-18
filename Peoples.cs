using System;
using System.Net;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class Peoples : Form
    {
        public Peoples()
        {
            InitializeComponent();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editPeople nc = new editPeople();
            if (nc.ShowDialog() == DialogResult.OK)
            {
                String people = addPeople(nc.txtFio.Text, nc.dtBirth.Value, (short)nc.cbSex.SelectedIndex, Convert.ToUInt64(nc.tvAddress.SelectedNode.Tag));

                ulong id = Convert.ToUInt64(people.Substring(people.IndexOf("\"id\":") + 5, people.IndexOf(",", people.IndexOf("\"id\":") + 5) - people.IndexOf("\"id\":") - 5));
                String fio = people.Substring(people.IndexOf("\"fio\":") + 7, people.IndexOf("\",", people.IndexOf("\"fio\":") + 7) - people.IndexOf("\"fio\":") - 7);
                short isMale = Convert.ToInt16(people.Substring(people.IndexOf("\"male\":") + 7, people.IndexOf(",", people.IndexOf("\"male\":") + 7) - people.IndexOf("\"male\":") - 7));
                String birth = people.Substring(people.IndexOf("\"birth\":") + 9, people.IndexOf("\"", people.IndexOf("\"birth\":") + 9) - people.IndexOf("\"birth\":") - 9);
                ulong aid = Convert.ToUInt64(people.Substring(people.IndexOf("\"aid\":") + 6, people.IndexOf("}", people.IndexOf("\"aid\":") + 6) - people.IndexOf("\"aid\":") - 6));

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = id;
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = fio });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = birth });//dd.MM.yyyy
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = isMale==1 ? 'М' : 'Ж' });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = aid });//TODO: load address as String

                dataGridView1.Rows.Add(row);
            }
        }
        
        String addPeople(String fio, DateTime birth, short male, ulong aid)
        {
            String x = birth.ToString();
            WebClient wc = new WebClient();
            String data = "fio=" + System.Web.HttpUtility.UrlEncode(fio) + "&birth=" + birth.ToString() +
                          "&male=" + male.ToString() + "&aid=" + aid.ToString();
            wc.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            String ret = System.Text.Encoding.UTF8.GetString(wc.UploadData(Program.BASE + "/people", System.Text.Encoding.Default.GetBytes(data)));
            return ret;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            ulong pid = Convert.ToUInt64(row.Tag);
            String data = "id=" + pid.ToString();
            try
            {
                WebRequest r = WebRequest.Create(Program.BASE + "/people");
                r.ContentType = "application/x-www-form-urlencoded";
                r.Method = "DELETE";
                r.GetRequestStream().Write(System.Text.Encoding.Default.GetBytes(data), 0, data.Length);
                if (((HttpWebResponse)r.GetResponse()).StatusCode == HttpStatusCode.NotFound)
                    dataGridView1.Rows.Remove(row);
            }
            catch (WebException wex)
            {
                if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                    dataGridView1.Rows.Remove(row);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void filterFio_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                ApplyFilter();
        }

        void ApplyFilter()
        {
            if ("" == filterFio.Text)//TODO: check spaces & other specials
                return;
            dataGridView1.Rows.Clear();
            WebClient wc = new WebClient();
            String json = wc.DownloadString(Program.BASE + "/people?fio=" + filterFio.Text);
            json = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(json));
            if ("[]".Equals(json))
                return;
            String[] peoples = json.Split(new String[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String people in peoples)
            {
                ulong id = Convert.ToUInt64(people.Substring(people.IndexOf("\"id\":") + 5, people.IndexOf(",", people.IndexOf("\"id\":") + 5) - people.IndexOf("\"id\":") - 5));
                String fio = people.Substring(people.IndexOf("\"fio\":") + 7, people.IndexOf("\",", people.IndexOf("\"fio\":") + 7) - people.IndexOf("\"fio\":") - 7);
                short isMale = Convert.ToInt16(people.Substring(people.IndexOf("\"male\":") + 7, people.IndexOf(",", people.IndexOf("\"male\":") + 7) - people.IndexOf("\"male\":") - 7));
                String birth = people.Substring(people.IndexOf("\"birth\":") + 9, people.IndexOf("\"", people.IndexOf("\"birth\":") + 9) - people.IndexOf("\"birth\":") - 9);
                ulong aid = Convert.ToUInt64(people.Substring(people.IndexOf("\"aid\":") + 6).Replace("]","").Replace("}", ""));

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = id;
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = fio });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = birth });//dd.MM.yyyy
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = isMale == 1 ? 'М' : 'Ж' });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = aid });//TODO: load address as String

                dataGridView1.Rows.Add(row);
            }
        }
    }
}
