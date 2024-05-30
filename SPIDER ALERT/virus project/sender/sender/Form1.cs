using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ListNetworkComputers;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;

namespace sender
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("server=.\\sqlexpress;Database=Virusspred;Integrated Security=true;");
        SqlCommand cmd = new SqlCommand();
        //SqlConnection con = new SqlConnection("server=.;integrated security=true;database=node;");
        //SqlCommand cmd;
        //SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }
        string uname, dest;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                NetworkBrowser nb = new NetworkBrowser();
                foreach (string pc in nb.getNetworkComputers())
                {
                    comboBox1.Items.Add(pc);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred trying to access the network computers", "error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }
        NetworkStream ns5;
        TcpClient Server5;
        TcpClient Server1 = new TcpClient();
        int portno;
        private void button1_Click(object sender, EventArgs e)
        {
            string fname;
          
            fname = textBox1.Text;
            string content = File.ReadAllText(fname);

            cmd = new SqlCommand("insert into fileinfo values('" + fname + "','" + System.DateTime.Now.ToShortDateString() + "','" + comboBox1.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            string port = "8888";
            cmd = new SqlCommand("insert into Systeminfo values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + port + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            try
            {
                if (true)          //!fname.EndsWith(".exe")
                {                   
                    TcpClient cli = new TcpClient("localhost", 1111);
                    NetworkStream ns = cli.GetStream();
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(ns, comboBox2.Text);
                    Thread.Sleep(500);
                    bf.Serialize(ns,comboBox1.Text);
                    Thread.Sleep(500);
                    bf.Serialize(ns, openFileDialog1.FileName);
                    Thread.Sleep(500);
                    bf.Serialize(ns, richTextBox1.Text);
                    Control.CheckForIllegalCrossThreadCalls = false;
                   
                    ns.Close();
                    cli.Close();

                    ////node
                    //int node = 0;
                    //string inputs1 = comboBox1.Text;
                    //int gii = fname.LastIndexOf("\\");


                    //Server5 = new TcpClient(ipaddr.ToString(), node);
                    //ns5 = Server5.GetStream();
                    //string input11 = fname.Substring(gii + 1);
                    //string input22 = Dns.GetHostName().ToString();
                    //string dest11 = "\\\\" + inputs1 + "\\Node\\" + input11.ToString();
                    ////   ns5.Write(Encoding.ASCII.GetBytes(input22), 0, input22.Length);

                    //ns5.Write(Encoding.ASCII.GetBytes(content), 0, content.Length);
                    //ns5.Write(Encoding.ASCII.GetBytes(input22), 0, input22.Length);
                    //Thread.Sleep(500);
                    //ns5.Write(Encoding.ASCII.GetBytes(inputs1), 0, inputs1.Length);
                    //ns5.Flush();

                    //ns5.Close();
                    //Server1.Close();
                MessageBox.Show("Data Sent SuccessFully");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string f;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                f = textBox1.Text;
                richTextBox1.Text = File.ReadAllText(f);
            }
            else
            {
                MessageBox.Show("Please select any file to Transfer the data");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
