using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace sender
{
    public partial class ViewdataTransaction : Form
    {

        SqlConnection con = new SqlConnection("server=.\\sqlexpress;Database=Virusspred;Integrated Security=true;");
        SqlCommand cmd = new SqlCommand();
        public ViewdataTransaction()
        {
            InitializeComponent();
        }

        private void ViewdataTransaction_Load(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from fileinfo", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "fileinfo");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            con.Close();


            cmd = new SqlCommand("select * from Systeminfo", con);
            con.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "Systeminfo");
            dataGridView2.DataSource = ds1.Tables[0].DefaultView;
            con.Close();
        }
    }
}
