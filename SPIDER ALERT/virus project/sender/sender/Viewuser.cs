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
    public partial class Viewuser : Form
    {
        SqlConnection con = new SqlConnection("server=.\\sqlexpress;Database=Virusspred;Integrated Security=true;");
        SqlCommand cmd = new SqlCommand();
        public Viewuser()
        {
            InitializeComponent();
        }

        private void Viewuser_Load(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from Usertbl", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Usertbl");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            con.Close();
        }
    }
}
