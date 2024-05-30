using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace node
{
    public partial class Monitoring : Form
    {
        private BackgroundWorker _BackgroundWorker;
        private Random _Random;
        public Monitoring()
        {
            InitializeComponent();

            //_ProgressBar.Style = ProgressBarStyle.Marquee;
            //_ProgressBar.Visible = false;
            //_Random = new Random();

            //InitializeBackgroundWorker();

        }

        private void InitializeBackgroundWorker()
        {
            _BackgroundWorker = new BackgroundWorker();
            _BackgroundWorker.WorkerReportsProgress = true;

            _BackgroundWorker.DoWork += (sender, e) => ((MethodInvoker)e.Argument).Invoke();
            _BackgroundWorker.ProgressChanged += (sender, e) =>
            {
                _ProgressBar.Style = ProgressBarStyle.Continuous;
                _ProgressBar.Value = e.ProgressPercentage;
            };
            _BackgroundWorker.RunWorkerCompleted += (sender, e) =>
            {
                if (_ProgressBar.Style == ProgressBarStyle.Marquee)
                {
                  //  _ProgressBar.Visible = false;
                }
            };
        }

        private string RemoteAddress;
        private void Monitoring_Load(object sender, EventArgs e)
        {
           // timer1.Enabled = true;

            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;


            _ProgressBar.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            backgroundWorker1.RunWorkerAsync();
          
            ////

            //string inputs = comboBox1.Text;
            //int gi = fname.LastIndexOf("\\");
            ////NetworkStream ns5;
            ////string fname = richTextBox1.Text;
            //Server5 = new TcpClient("" + comboBox1.Text + "", 9999);
            //ns5 = Server5.GetStream();
            //string input1 = fname.Substring(gi + 1);
            //string input2 = Dns.GetHostName().ToString();
            //string dest1 = "\\\\" + inputs + "\\SSPU\\" + input1.ToString();
            //ns5.Write(Encoding.ASCII.GetBytes(input2), 0, input2.Length);
            //ns5.Write(Encoding.ASCII.GetBytes(content), 0, content.Length);
            //ns5.Flush();

            //ns5.Close();
            //Server1.Close();
            //if (File.Exists(dest1))
            //    File.Delete(dest1);
            //File.Copy(fname, dest1);
        }
        int timerCount = 0;
        public void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (timerCount % 2 == 0)
                //Do you works
                timerCount++;
            progressBar1.Value = timerCount;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           ReceiveDet1(e);
          
        }
        public void ReceiveDet1(DoWorkEventArgs e)
        {
            try
            {

                TcpListener list = new TcpListener(IPAddress.Any, 1111);
                list.Start();

               

                Socket soc = list.AcceptSocket();
                string remote = soc.RemoteEndPoint.ToString();
                string[] sp = remote.Split(':');
                RemoteAddress = sp[0];

                NetworkStream ns = new NetworkStream(soc);
                BinaryFormatter bf = new BinaryFormatter();

                progressBar1.Value = 0;
                progressBar2.Value = 0;
                progressBar3.Value = 0;
                Control.CheckForIllegalCrossThreadCalls = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;

                object ports = bf.Deserialize(ns);
                Thread.Sleep(500);
                object desti = bf.Deserialize(ns);
                Thread.Sleep(500);
                object fname = bf.Deserialize(ns);
                Thread.Sleep(500);
                object data = bf.Deserialize(ns);

                Control.CheckForIllegalCrossThreadCalls = false;
                lblPORT.Text = ports.ToString();
                lblDestination.Text = desti.ToString();
                lblFileName.Text = fname.ToString();
                lblData.Text = data.ToString();

                list.Stop();
                soc.Close();
                ns.Close();

                if (lblFileName.Text.EndsWith(".txt"))
                {
                    if (ports.ToString() == "Node A")
                    {

                        e.Cancel = true;

                        timer1.Enabled = true;
                        timer1_Tick(null, e);
                        MessageBox.Show("Data send Successfully through Node A");
                        Thread.Sleep(2000);


                    }
                    else if (ports.ToString() == "Node B")
                    {

                        e.Cancel = true;

                        timer2.Enabled = true;
                        timer2_Tick(null, e);
                        MessageBox.Show("Data send Successfully through Node B");
                        Thread.Sleep(2000);

                    }
                    else if (ports.ToString() == "Node C")
                    {
                        e.Cancel = true;

                        timer3.Enabled = true;
                        timer3_Tick(null, e);
                        MessageBox.Show("Data send Successfully through Node C");
                        Thread.Sleep(2000);
                    }


                    MessageSend(label2.Text, lblData.Text, "MessageTrans", 2000);


                }
                else if (lblFileName.Text.EndsWith(".exe"))
                {  
                    e.Cancel = true;
                    timer.Enabled = true;
                    timer_Tick(null, e);
                    Thread.Sleep(2000);
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = true;
                   
                    Thread.Sleep(2000);
                }
                //    timer1.Enabled = true;
                // timer1_Tick(null, null);

                if (soc.Connected == false)
                {
                    ReceiveDet1(e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void MessageSend(string IPaddr, string MessDet, string command, int port)
        {

            TcpClient cli = new TcpClient(lblDestination.Text, port);
            NetworkStream ns = cli.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, Environment.MachineName.ToString());
            bf.Serialize(ns, lblData.Text);
            ns.Close();
            cli.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = progressBar1.Value + 1;
            //while (progressBar1.Value <= 90)
            //{
                if (progressBar1.Value <= 99)
                {
                    Thread.Sleep(100);
                    timer1_Tick(sender, e);
                   
                }
                else
                {
                  
                  //  backgroundWorker1.RunWorkerAsync();
                    timer1.Stop();
                }
            //}

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar2.Value = progressBar2.Value + 1;
            //while (progressBar1.Value <= 90)
            //{
            if (progressBar2.Value <= 99)
            {
                Thread.Sleep(100);
                timer2_Tick(sender, e);

            }
            else
            {

                //  backgroundWorker1.RunWorkerAsync();
                timer2.Stop();
            }
            //}
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            progressBar3.Value = progressBar3.Value + 1;
            //while (progressBar1.Value <= 90)
            //{
            if (progressBar3.Value <= 99)
            {
                Thread.Sleep(100);
                timer3_Tick(sender, e);

            }
            else
            {

                //  backgroundWorker1.RunWorkerAsync();
                timer3.Stop();
            }
            //}
        }

        private void progressBar3_Click(object sender, EventArgs e)
        {

        }
        string portss;
        private void timer_Tick(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            portss=lblPORT.Text ;
            if (portss == "Node A")
            {
                progressBar1.Value = progressBar1.Value + 2;
                if (progressBar1.Value <= 50)
                {
                    Thread.Sleep(200);
                    timer_Tick(sender, e);

                }
                else
                {

                    //  backgroundWorker1.RunWorkerAsync();
                    timer.Stop();
                }
            }
            else
            {
                progressBar1.Value = progressBar1.Value + 1;
            }

            if (portss == "Node B")
            {
                progressBar2.Value = progressBar2.Value + 2;
                if (progressBar2.Value <= 50)
                {
                    Thread.Sleep(200);
                    timer_Tick(sender, e);

                }
                else
                {

                    //  backgroundWorker1.RunWorkerAsync();
                    timer.Stop();
                }
            }
            else
            {
                progressBar2.Value = progressBar2.Value + 1;
            }

            if (portss == "Node C")
            {
                progressBar3.Value = progressBar3.Value + 2;
                if (progressBar3.Value <= 50)
                {
                    Thread.Sleep(200);
                    timer_Tick(sender, e);

                }
                else
                {

                    //  backgroundWorker1.RunWorkerAsync();
                    timer.Stop();
                }
            }
            else
            {
                progressBar3.Value = progressBar3.Value + 1;
            }

            //while (progressBar1.Value <= 90)
            //{
          
           
            //}
        }

      

    }
}
