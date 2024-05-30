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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("server=.\\sqlexpress;Database=Virusspred;Integrated Security=true;");
        SqlCommand cmd = new SqlCommand();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Admin" && textBox2.Text == "Admin")
            {
                //this.Close();
                Adminhome fadm = new Adminhome();
                fadm.Show();
           
            }
            else
            {
                cmd = new SqlCommand("select * from Usertbl where Uid='" + textBox1.Text + "' and Pwd='" + textBox2.Text + "'", con);
                con.Close();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Form1 fusr = new Form1();
                    fusr.Show();
                }
                else
                {
                    MessageBox.Show("Login faild");

                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Newuser ob = new Newuser();
            ob.Show();
        }
    }
}
