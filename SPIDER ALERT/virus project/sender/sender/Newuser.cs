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
    public partial class Newuser : Form
    {
        SqlConnection con = new SqlConnection("server=.\\sqlexpress;Database=Virusspred;Integrated Security=true;");
        SqlCommand cmd = new SqlCommand();
        public Newuser()
        {
            InitializeComponent();
        }

        private void Newuser_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("insert into Usertbl values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Registerd..");
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("The User Name Already Exist. Please Try Other");
            }
        }
    }
}
