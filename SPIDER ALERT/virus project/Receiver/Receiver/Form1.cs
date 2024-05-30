using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.IO;

namespace Receiver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ready To Receive Data..");
            backgroundWorker1.RunWorkerAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        public void ReceiveEnrouteDetails()
        {
            TcpListener list = new TcpListener(IPAddress.Any, 2000);
            list.Start();
            Socket soc = list.AcceptSocket();
            string remote = soc.RemoteEndPoint.ToString();
            NetworkStream ns = new NetworkStream(soc);
            BinaryFormatter bf = new BinaryFormatter();
            object source = bf.Deserialize(ns);
            
            object message =  bf.Deserialize(ns);           
            Control.CheckForIllegalCrossThreadCalls = false;
            textBox1.Text = source.ToString();

            richTextBox1.Text = message.ToString();

         
            list.Stop();
            soc.Close();
            ns.Close();
            if (soc.Connected == false)
            {
                ReceiveEnrouteDetails();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ReceiveEnrouteDetails();
        }

    }
}
