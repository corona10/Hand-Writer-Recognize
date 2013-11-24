using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
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

        public void run(CPacket packet)
        {
            try
            {
                //Create an instance of TcpClient. 
                TcpClient tcpClient = new TcpClient(this._server, this._tcpPort);
                //Create a NetworkStream for this tcpClient instance. 
                //This is only required for TCP stream. 
              
                NetworkStream tcpStream = tcpClient.GetStream();

                if (tcpStream.CanWrite)
                {
                    
                    string json = Utility.func_WriteJson(packet);
                    byte[] jsontest = Utility.func_Json2Byte(json);
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
                tcpStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception has occurred.");
                Console.WriteLine(e.ToString());
            }

        }
    }
}
