﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Windows.Forms;

namespace vAdmin
{
    public partial class Rivals : Form
    {
        public Rivals()
        {
            InitializeComponent();
        }

        private void Rivals_Load(object sender, EventArgs e)
        {
            List<Pair<ulong, String>> voteList = new List<Pair<ulong, string>>();
            WebClient wc1 = new WebClient();
            String json1 = wc1.DownloadString("http://10.0.0.100/vote");
            json1 = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(json1));
            String[] votes = json1.Split(new String[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String vote in votes)
            {
                ulong id = Convert.ToUInt64(vote.Substring(vote.IndexOf("\"id\":") + 5, vote.IndexOf(",", vote.IndexOf("\"id\":") + 5) - vote.IndexOf("\"id\":") - 5));
                String name = vote.Substring(vote.IndexOf("\"name\":") + 8, vote.IndexOf("\",", vote.IndexOf("\"name\":") + 8) - vote.IndexOf("\"name\":") - 8);
                voteList.Add(new Pair<ulong, String>(id, name));
            }
            WebClient wc = new WebClient();
            String json = wc.DownloadString("http://10.0.0.100/rival");
            json = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(json));
            String[] rivals = json.Split(new String[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String rival in rivals)
            {
                ulong id = Convert.ToUInt64(rival.Substring(rival.IndexOf("\"id\":") + 5, rival.IndexOf(",", rival.IndexOf("\"id\":") + 5) - rival.IndexOf("\"id\":") - 5));
                String name = rival.Substring(rival.IndexOf("\"name\":") + 8, rival.IndexOf("\",", rival.IndexOf("\"name\":") + 8) - rival.IndexOf("\"name\":") - 8);
                String description = rival.Substring(rival.IndexOf("\"description\":") + 15, rival.IndexOf("\"", rival.IndexOf("\"description\":") + 15) - rival.IndexOf("\"description\":") - 15);
                short position = Convert.ToInt16(rival.Substring(rival.IndexOf("\"position\":") + 11, rival.IndexOf(",", rival.IndexOf("\"position\":") + 11) - rival.IndexOf("\"position\":") - 11));
                ulong vid = Convert.ToUInt64(rival.Substring(rival.IndexOf("\"vid\":") + 6, rival.IndexOf("}", rival.IndexOf("\"vid\":") + 6) - rival.IndexOf("\"vid\":") - 6));

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = id;
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = name });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = description });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = voteList.Find(V => V.a == vid).b });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = position == 0 ? "-" : position.ToString() });

                dataGridView1.Rows.Add(row);
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editRival nc = new editRival();
            if (nc.ShowDialog() == DialogResult.OK)
            {
                Pair<ulong, String> selectedVote = (Pair<ulong, String>)nc.cbVote.SelectedItem;
                short _position = nc.cbPosition.SelectedText.Length > 0 ? Convert.ToInt16(nc.cbPosition.SelectedText) : (short)0;
                String rival = addRival(nc.txtName.Text, nc.txtDescription.Text, _position, selectedVote.a);

                ulong id = Convert.ToUInt64(rival.Substring(rival.IndexOf("\"id\":") + 5, rival.IndexOf(",", rival.IndexOf("\"id\":") + 5) - rival.IndexOf("\"id\":") - 5));
                String name = rival.Substring(rival.IndexOf("\"name\":") + 8, rival.IndexOf("\",", rival.IndexOf("\"name\":") + 8) - rival.IndexOf("\"name\":") - 8);
                String description = rival.Substring(rival.IndexOf("\"description\":") + 15, rival.IndexOf("\"", rival.IndexOf("\"description\":") + 15) - rival.IndexOf("\"description\":") - 15);
                short position = Convert.ToInt16(rival.Substring(rival.IndexOf("\"position\":") + 11, rival.IndexOf(",", rival.IndexOf("\"position\":") + 11) - rival.IndexOf("\"position\":") - 11));
                ulong vid = Convert.ToUInt64(rival.Substring(rival.IndexOf("\"vid\":") + 6, rival.IndexOf("}", rival.IndexOf("\"vid\":") + 6) - rival.IndexOf("\"vid\":") - 6));

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = id;
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = name });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = description });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = vid });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = position == 0 ? "-" : position.ToString() });

                dataGridView1.Rows.Add(row);
            }
        }

        String addRival(String name, String description, short position, ulong vid)
        {
            WebClient wc = new WebClient();
            String data = "name=" + System.Web.HttpUtility.UrlEncode(name) + "&description=" + System.Web.HttpUtility.UrlEncode(description) +
                          "&position=" + position.ToString() + "&vid=" + vid.ToString();
            wc.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            String ret = System.Text.Encoding.UTF8.GetString(wc.UploadData("http://10.0.0.100/rival", System.Text.Encoding.Default.GetBytes(data)));
            return ret;
        }
    }
}
