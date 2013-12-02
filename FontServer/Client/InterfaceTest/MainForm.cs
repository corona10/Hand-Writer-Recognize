/**
 * The MIT License (MIT)

Copyright (c) 2013 Dong-hee,Na <corona10@gmail.com> Jun-woo, Choi <choigo92@gmail.com>  Sun-min, Kim <mh5537@naver.com>

 Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
 ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * **/
using System;
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

        private void sendButton_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            List<int> direction = new List<int>();

            _server = IP_TextBox.Text;
            _tcpPort = Convert.ToInt32(portTextBox.Text);
            
            // Calculate Direction
            for (int i = 0; i < _savedPointX.Count - 1; i++)
            {
                int tempDirection = Direction.GetDirection(_savedPointX[i], _savedPointY[i], _savedPointX[i + 1], _savedPointY[i + 1]);
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
                            Thread th = new Thread(new ParameterizedThreadStart(run));
                            th.Start(packet);
                        }
                        else if (Convert.ToChar(infoTextBox.Text) >= 'A' && Convert.ToChar(infoTextBox.Text) <= 'Z')
                        {
                            CPacket packet = new CPacket(CPacket.Kind.TRAINING_SET, (CPacket.ValueKind)ValueKindMap.mapping((int)Convert.ToChar(infoTextBox.Text) - 55), direction.ToArray());
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

        private void func_text(string _text)
        {
            textBox.Text = _text;
        }

        private delegate void thDelegate(string _text);

        private void run(object packet)
        {
            try
            {
                //Create an instance of TcpClient. 
                TcpClient tcpClient = new TcpClient(this._server, this._tcpPort);
                //Create a NetworkStream for this tcpClient instance. 
                //This is only required for TCP stream. 
                DONE = false;
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

                        if ((ValueKindMap.ValueKind)test_read_packet._value >= ValueKindMap.ValueKind.ONE
                            && (ValueKindMap.ValueKind)test_read_packet._value <= ValueKindMap.ValueKind.NINE)
                        {
                            //textBox4에 출력
                            _text += ValueKindMap.GetIndex((ValueKindMap.ValueKind)test_read_packet._value);
                            //textBox.Text = _text;
                            this.Invoke(new thDelegate(func_text), _text);
                            MessageBox.Show("인식값: " + ValueKindMap.GetIndex((ValueKindMap.ValueKind)test_read_packet._value));
                            DONE = true;
                        }
                        else if ((ValueKindMap.ValueKind)test_read_packet._value >= ValueKindMap.ValueKind.A
                            && (ValueKindMap.ValueKind)test_read_packet._value <= ValueKindMap.ValueKind.z)
                        {
                            //textBox4에 출력
                            _text += test_read_packet._value.ToString();
                            //textBox.Text = _text;
                            this.Invoke(new thDelegate(func_text), _text);
                            MessageBox.Show("인식값: " + test_read_packet._value.ToString());
                            DONE = true;
                        }
                        else if (trainRadioButton.Checked)
                        {
                            MessageBox.Show("Training 완료");
                            DONE = true;
                        }
                        else
                        {
                            MessageBox.Show("인식 실패");
                            DONE = true;
                        }
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
        private void analysisRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            infoTextBox.Enabled = false;
        }

        //Training RadioButton
        private void trainRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            infoTextBox.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Clear Button
        private void clearButton_Click(object sender, EventArgs e)
        {
            panel.Refresh();
            _count = 0;
            _savedPointX.Clear();
            _savedPointY.Clear();
        }

        //Save Button
        private void saveButton_Click(object sender, EventArgs e)
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
            } 
        }

        //Delete All Button
        private void deleteButton_Click(object sender, EventArgs e)
        {
            _text = null;
            textBox.Text = _text;
        }
    }
}
