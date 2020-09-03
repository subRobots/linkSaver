using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace linkSaver
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addLink(txtURL.Text, txtName.Text);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                lstLinks.DataSource = Properties.Settings.Default.listboxItems.Cast<string>().ToArray();
                lstLinks.SelectedIndex = -1;
                lblCount.Text = (lstLinks.Items.Count).ToString();
            }

            catch
            {
                Console.WriteLine("Error!");
            }
        }

        private void lstLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCount.Text = (lstLinks.Items.Count ).ToString();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
        private void addLink(string targetURL, string targetName)
        {
            string setting = targetName + "," + targetURL;
            Properties.Settings.Default.listboxItems.Add(setting);

            lstLinks.DataSource = Properties.Settings.Default.listboxItems.Cast<string>().ToArray();
            lstLinks.SelectedIndex = -1;
            txtName.Text = "";
            txtURL.Text = "";
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            deleteSelectedItem(lstLinks);
        }
        private void deleteSelectedItem(ListBox targetListbox)
        {
            Properties.Settings.Default.listboxItems.Remove(lstLinks.GetItemText(lstLinks.SelectedItem));
            targetListbox.Items.Remove(lstLinks.SelectedItems);
            targetListbox.DataSource = null;
            targetListbox.DataSource = Properties.Settings.Default.listboxItems.Cast<string>().ToArray();
            targetListbox.Refresh();
        }
        private void gotoURL()
        {
            string[] parseURL = lstLinks.GetItemText(lstLinks.SelectedItem).Split(',');
            Process.Start("chrome.exe", parseURL[1]);
        }
        private void cmdGo_Click(object sender, EventArgs e)
        {
            gotoURL();
        }

        private void lstLinks_DoubleClick(object sender, EventArgs e)
        {
            gotoURL();
        }
    }
}
