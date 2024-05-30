using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sender
{
    public partial class Adminhome : Form
    {
        public Adminhome()
        {
            InitializeComponent();
        }

        private void viewDataTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewdataTransaction ob = new ViewdataTransaction();
            ob.Show();
        }

        private void userInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Viewuser ob = new Viewuser();
            ob.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
