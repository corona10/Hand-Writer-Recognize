/**
 @section LICENSE
 
 The MIT License (MIT)

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using fontWriter;
using Newtonsoft.Json;
using System.Collections;

namespace fontWrtier
{
    class TcpClientModule
    {
        private int _tcpPort;
        private string _server;
        private bool DONE = false;


        public TcpClientModule(String server, int port)
        {
            this._server = server;
            this._tcpPort = port;
        }

        public void run()
        {
            try
            {
                
                //Create an instance of TcpClient. 
                TcpClient tcpClient = new TcpClient(this._server, this._tcpPort);
                //Create a NetworkStream for this tcpClient instance. 
                //This is only required for TCP stream. 
                Console.WriteLine("클라이언트 초기화");
                NetworkStream tcpStream = tcpClient.GetStream();
                while (true)
                {
                    if (tcpStream.CanWrite)
                    {
                        Console.WriteLine("Request(0) / Traing(1) / NULL(2) ");
                        string test = Console.ReadLine();
                        int select = Convert.ToInt32(test);
                        Console.WriteLine("배열 길이? ");
                        int length = Convert.ToInt32(Console.ReadLine());
                        List<int> array = new List<int>();
                        Console.WriteLine("값을 입력하시오:");
                        for (int i = 0; i < length; i++)
                        {
                            array.Add(Convert.ToInt32(Console.ReadLine()));
                        }
                        Console.WriteLine("클라이언트 메시지 작성중");
                        int[] testarray = array.ToArray();
                        CPacket testPacket = new CPacket(CPacket.Kind.NONE, -1, testarray);
                        if (select == 1)
                        {
                            Console.WriteLine("어떤 값을 트레이닝 시킬지 입력하시오.");
                            int val = Convert.ToInt32(Console.ReadLine());
                            testPacket = new CPacket(CPacket.Kind.TRAINING_SET, val, testarray);
                        }
                        else if (select == 0)
                        {
                            testPacket = new CPacket(CPacket.Kind.REQUEST, -1, testarray);
                        }
                        else if (select == 2)
                        {
                            testPacket = null;
                        }
                        string json = Utility.func_WriteJson(testPacket);
                        byte[] jsontest = Utility.func_Json2Byte(json);
                        Console.WriteLine("클라이언트 메시지 작성 완료");
                        Utility.func_DisplayPacketInfo(testPacket);
                        Console.WriteLine(json);
                        Console.WriteLine("메시지 사이즈: " + jsontest.Length);
                        tcpStream.Write(jsontest, 0, json.Length);
                        tcpStream.Flush();
                        Console.WriteLine("클라이언트 메시지 전송 성공");
                    }
                    while (tcpStream.CanRead && !DONE)
                    {
                        //We need the DONE condition here because there is possibility that 
                        //the stream is ready to be read while there is nothing to be read. 
                        if (tcpStream.DataAvailable)
                        {
                            Byte[] received = new Byte[1024];
                            int nBytesReceived = tcpStream.Read(received, 0, received.Length);
                            Console.WriteLine("서버로부터 데이터 수신 완료");
                            CPacket test_read_packet = Utility.func_ReadJson(received);
                            Utility.func_DisplayPacketInfo(test_read_packet);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception has occurred.");
                Console.WriteLine(e.ToString());
            }
        }

        
    }
}
