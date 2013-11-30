﻿using System;
using System.IO;
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
        private bool _mouseClick = false;
        private List<int> _savedPointX = new List<int>();
        private List<int> _savedPointY = new List<int>();
        private int _tcpPort = 0;
        private string _server = null;
        private bool DONE = false;
        private int _count=0;
        private string _text = null;

        int _pX = -1;
        int _pY = -1;

        public MainForm()
        {
            InitializeComponent();
            
            this.panel.Paint += new PaintEventHandler(Panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
        }
        void Panel_Paint(object sender, PaintEventArgs s)
        {
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseClick = true;
            this._pX = e.X;
            this._pY = e.Y;
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseClick = false;
        }
        
        //Drawing
        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseClick == true)
            {
                
                Panel frm = (Panel)sender;
                Graphics g = frm.CreateGraphics();
                
                Pen pen = new Pen(Color.Black, 14);

                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                
                g.DrawLine(pen, _pX, _pY, e.X, e.Y);
                
                g.Dispose();
                            
                _savedPointX.Add(e.X);
                _savedPointY.Add(e.Y);
                _count++;
            }
            _pX = e.X;
            _pY = e.Y;
        }

        /*
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            List<int> direction = new List<int>();

            _server = IP_TextBox.Text;
            _tcpPort = Convert.ToInt32(portTextBox.Text);
            
            // Calculate Direction
            for (int i = 0; i < _savedPointX.Count - 1; i++)
            {
                int tempDirection = GetDirection(_savedPointX[i], _savedPointY[i], _savedPointX[i + 1], _savedPointY[i + 1]);
                if (tempDirection != -1)
                {
                    direction.Add(tempDirection);
                    
                }
                else
                {
                    MessageBox.Show("잘못된 방향값입니다.");
                    direction.Clear();
                    g.Clear(System.Drawing.SystemColors.Control);
                    _savedPointX.Clear();
                    _savedPointY.Clear();
                    return;
                }
            }
            //Check Training Mode 
            if (trainRadioButton.Checked)
            {
                try
                {
                    if ((Convert.ToInt32(infoTextBox.Text) >= 0 && Convert.ToInt32(infoTextBox.Text) <= 9))
                    {
                        CPacket packet = new CPacket(CPacket.Kind.TRAINING_SET, (CPacket.ValueKind)ValueKindMap.mapping(Convert.ToInt32(infoTextBox.Text)), direction.ToArray());
                        MessageBox.Show("ValueKind : " + (CPacket.ValueKind)ValueKindMap.mapping(Convert.ToInt32(infoTextBox.Text)));
                        Thread th = new Thread(new ParameterizedThreadStart(run));
                        th.Start(packet);
                    }
                    else
                        MessageBox.Show("Training Info Error");
                }
                catch (Exception e1)
                {
                    try
                    {
                        if ( Convert.ToChar(infoTextBox.Text) >= 'a' && Convert.ToChar(infoTextBox.Text) <= 'z')
                        {
                            CPacket packet = new CPacket(CPacket.Kind.TRAINING_SET, (CPacket.ValueKind)ValueKindMap.mapping((int)Convert.ToChar(infoTextBox.Text)-61), direction.ToArray());
                            MessageBox.Show("ValueKind : " +  (CPacket.ValueKind)ValueKindMap.mapping((int)Convert.ToChar(infoTextBox.Text)-61));
                            Thread th = new Thread(new ParameterizedThreadStart(run));
                            th.Start(packet);
                        }
                        else if (Convert.ToChar(infoTextBox.Text) >= 'A' && Convert.ToChar(infoTextBox.Text) <= 'Z')
                        {
                            CPacket packet = new CPacket(CPacket.Kind.TRAINING_SET, (CPacket.ValueKind)ValueKindMap.mapping((int)Convert.ToChar(infoTextBox.Text) - 55), direction.ToArray());
                            MessageBox.Show("ValueKind : " + (CPacket.ValueKind)ValueKindMap.mapping((int)Convert.ToChar(infoTextBox.Text) - 55));
                            Thread th = new Thread(new ParameterizedThreadStart(run));
                            th.Start(packet);
                        }
                        else
                            MessageBox.Show("Training Info Error");
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("Training Info Error");
                    }
                }
            }

            //Check Analysis Mode
            else if (analysisRadioButton.Checked)
            {
                CPacket packet = new CPacket(CPacket.Kind.REQUEST, CPacket.ValueKind.NONE, direction);
                Thread th = new Thread(new ParameterizedThreadStart(run));
                th.Start(packet);
                //MessageBox.Show();
            }
            //Don`t checking
            else
            {
                MessageBox.Show("radioButton을 check해주세요.");
            }

            //Clear
            panel.Refresh();
            _count = 0;
            _savedPointX.Clear();
            _savedPointY.Clear();
        }

        private int GetDirection(int x1, int y1, int x2, int y2)
        {
            //int direction=0;
            double x3, y3;
            //double angle;
            x3 = x2 - x1;
            y3 = y1 - y2;
            double angle = Math.Atan((double)y3 / (double)Math.Abs(x3));
            if (x3 < 0)
            {
                if (angle < -Math.PI * 3 / 8)
                {
                    return 6;
                }
                else if (angle >= -Math.PI * 3 / 8 && angle < -Math.PI / 8)
                {
                    return 5;
                }
                else if (angle >= -Math.PI / 8 && angle < Math.PI / 8)
                {
                    return 4;
                }
                else if (angle >= Math.PI / 8 && angle < Math.PI * 3 / 8)
                {
                    return 3;
                }
                else if (angle >= Math.PI * 3 / 8)
                {
                    return 2;
                }
            }
            else
            {
                if (angle < -Math.PI * 3 / 8)
                {
                    return 6;
                }
                else if (angle >= -Math.PI * 3 / 8 && angle < -Math.PI / 8)
                {
                    return 7;
                }
                else if (angle >= -Math.PI / 8 && angle < Math.PI / 8)
                {
                    return 0;
                }
                else if (angle >= Math.PI / 8 && angle < Math.PI * 3 / 8)
                {
                    return 1;
                }
                else if (angle >= Math.PI * 3 / 8)
                {
                    return 2;
                }
            }
            /*
            x3 = x2 - x1;
            y3 = y1 - y2;



            if (x3 == 0)
            {
                x3 = 0.00000001;
            }
            if(y3>=0 && x3 >=0)
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
            */
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
            return -1;
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

                        //textBox4에 출력
                        _text += test_read_packet._value.ToString();
                        textBox.Text = _text;
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

        //Analysis RadioButton
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            infoTextBox.Enabled = false;
        }

        //Training RadioButton
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            infoTextBox.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Clear Button
        private void button3_Click(object sender, EventArgs e)
        {
            panel.Refresh();
            _count = 0;
            _savedPointX.Clear();
            _savedPointY.Clear();
        }

        //Save Button
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = @"C:\";
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text Files (*.txt)|*.txt; | All files (*.*)|*.*;";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFile.FileName, true, Encoding.Default);

                streamWriter.Write(textBox.Text);

                streamWriter.Close();

                MessageBox.Show("저장 완료");
            } 
        }

        //Delete All Button
        private void button5_Click(object sender, EventArgs e)
        {
            _text = null;
            textBox.Text = _text;
        }
    }
}
