using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace InterfaceTest
{
    public partial class MainForm : Form
    {
        private bool MouseClick = false;
        private List<int> SavedPointX = new List<int>();
        private List<int> SavedPointY = new List<int>();
        private int _tcpPort = 22;
        private string _server = "168.188.111.43";
        private bool DONE = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseClick = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseClick = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Drawing
            if (MouseClick == true)
            {
                Form frm = (MainForm)sender;
                Graphics g = frm.CreateGraphics();

                Bitmap bmOnePixel = new Bitmap(1, 1);
                bmOnePixel.SetPixel(0, 0, Color.Black);
                g.DrawImage(bmOnePixel, new Point(e.X, e.Y));
                g.Dispose();

                SavedPointX.Add(e.X);
                SavedPointY.Add(e.Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            List<int> Direction = new List<int>();

            // Calculate Direction
            for (int i = 0; i < SavedPointX.Count - 1; i++)
                Direction.Add(GetDirection(SavedPointX[i], SavedPointY[i], SavedPointX[i + 1], SavedPointY[i + 1]));

            //Check Training Mode 
            if (radioButton1.Checked)
            {
                if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 9)
                {

                    CPacket packet = new CPacket(CPacket.Kind.TRAINING_SET, Convert.ToInt32(textBox1.Text), Direction.ToArray());
                    Thread th = new Thread(new ParameterizedThreadStart(run));
                    th.Start(packet);
                }
                else
                    MessageBox.Show("error");
            }
            //Check Analysis Mode
            else if (radioButton2.Checked)
            {
                CPacket packet = new CPacket(CPacket.Kind.REQUEST, 1, Direction);
                Thread th = new Thread(new ParameterizedThreadStart(run));
                th.Start(packet);

            }
            //Don`t checking
            else
            {
                MessageBox.Show("radioButton을 check해주세요.");
            }

            g.Clear(System.Drawing.SystemColors.Control);
            SavedPointX.Clear();
            SavedPointY.Clear();
        }

        private int GetDirection(int x1, int y1, int x2, int y2)
        {
            int direction=0;
            double x3, y3;
            double angle;

            x3 = x2 - x1;
            y3 = y2 - y1;



            if (x3 == 0)
            {
                x3 = 0.00000001;
            }

            angle = Math.Atan(y3 / x3);

            if (0 <= angle && angle < Constants.PI / 4)
                direction = 0;
            else if (Constants.PI / 4 <= angle && angle < Constants.PI / 2)
                direction = 1;
            else if (Constants.PI / 2 <= angle && angle < Constants.PI * 3 / 4)
                direction = 2;
            else if (Constants.PI * 3 / 4 <= angle && angle < Constants.PI)
                direction = 3;
            else if (Constants.PI <= angle && angle < Constants.PI * 5 / 4)
                direction = 4;
            else if (Constants.PI * 5 / 4 <= angle && angle < Constants.PI * 3 / 2)
                direction = 5;
            else if (Constants.PI * 3 / 2 <= angle && angle < Constants.PI * 7 / 4)
                direction = 6;
            else if (Constants.PI * 7 / 4 <= angle && angle < Constants.PI * 2)
                direction = 7;

            /*
            if (x3 < 0)
                angle += (Constants.PI);

            if (x2 >= x1 && y2 >= y1)
                angle += 2 * Constants.PI;

            direction = (int)(angle * 8 / Constants.PI + 1) / 2;

            switch (direction)
            {
                case 0:
                case 8:
                    direction = 0;
                    break;
                case 1:
                    direction = 1;
                    break;
                case 2:
                    direction = 2;
                    break;
                case 3:
                    direction = 3;
                    break;
                case 4:
                    direction = 4;
                    break;
                case 5:
                    direction = 5;
                    break;
                case 6:
                    direction = 6;
                    break;
                case 7:
                    direction = 7;
                    break;
            }
            */
            return direction;
        }

        private void run(object packet)
        {
            try
            {
                //Create an instance of TcpClient. 
                TcpClient tcpClient = new TcpClient(this._server, this._tcpPort);
                //Create a NetworkStream for this tcpClient instance. 
                //This is only required for TCP stream. 

                CPacket pac = (CPacket)packet;
                NetworkStream tcpStream = tcpClient.GetStream();
                if (tcpStream.CanWrite)
                {

                    string json = Utility.func_WriteJson(pac);

                    byte[] jsontest = Utility.func_Json2Byte(json);
                    tcpStream.Write(jsontest, 0, json.Length);
                    tcpStream.Flush();
                    MessageBox.Show("클라이언트 메시지 전송 성공");
                }
                while (tcpStream.CanRead && !DONE)
                {
                    //We need the DONE condition here because there is possibility that 
                    //the stream is ready to be read while there is nothing to be read. 
                    if (tcpStream.DataAvailable)
                    {
                        Byte[] received = new Byte[1024];
                        int nBytesReceived = tcpStream.Read(received, 0, received.Length);

                        CPacket test_read_packet = Utility.func_ReadJson(received);

                        MessageBox.Show("인식값: " + test_read_packet._value.ToString());

                    }
                    Thread.Sleep(150);
                }
                tcpStream.Close();
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception has occurred.");
                Console.WriteLine(e.ToString());
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
