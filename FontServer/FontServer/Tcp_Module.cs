using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Collections;



namespace FontServer
{
    class Tcp_Module
    {

        
        public const int PACKET_SIZE = 1024;
        public const int MAXIMUM_USER = 50;

        private CHmmManager _cmm = CHmmManager.func_Instance();
        private CMongoManager _cMongo = CMongoManager.func_Instance();
        private static TcpListener _tcpListener;
        private CPacket _packet;
        private MemoryStream _ms;
        private JsonSerializer _serializer;
        private int tcp_port;
        private Thread _tcpThread;
        private Thread _dbThread;

        
        private int m_numConnections;   // the maximum number of connections the sample is designed to handle simultaneously 
        private int m_receiveBufferSize;// buffer size to use for each socket I/O operation 
        //BufferManager m_bufferManager;  // represents a large reusable set of buffers for all socket operations
        const int opsToPreAlloc = 2;    // read, write (don't alloc buffer space for accepts)
        //SocketAsyncEventArgsPool m_readWritePool;
        public Tcp_Module()
        {
            this._ms = new MemoryStream();
            this._serializer = new JsonSerializer();
        }

        public Tcp_Module(int port)
        {
            this._ms = new MemoryStream();
            this._serializer = new JsonSerializer();
            this.tcp_port = port;
            _tcpListener = new TcpListener(port);

            try
            {
                
                this._dbThread = new Thread(new ThreadStart(this._cMongo.func_Connect2MongoDB));
                this._tcpThread = new Thread(new ThreadStart(this.tcp));
                Console.WriteLine("* Try to Connect DB Server...");

                this._dbThread.Start();
                this._dbThread.Join();

                this._dbThread = new Thread(new ThreadStart(this.func_InitializeTrainingSet));
               
                this._dbThread.Start();
                this._dbThread.Join();

                this._tcpThread.Start();
                Console.Write("* Server Module Initialized.......... ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [SUCCESS]");
                Console.ResetColor();
               

            }
            catch (Exception e)
            {
                Console.Write("* Server Module Initialized.......... ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(" [FAILED]");
                Console.ResetColor();
            }

        }

       

        public void tcp()
        {
            _tcpListener.Start();

            for (int i = 0; i < MAXIMUM_USER; i++)
            {
                Thread multi_Listen = new Thread(new ThreadStart(func_Listen));
                multi_Listen.Start();
            }
            /**while (true)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.func_Listen));
            }**/
        }
        private void func_Listen()
        {

            try
            {
                while (true)
                {
                    
                        Socket tcp_socket = _tcpListener.AcceptSocket();
                        Console.WriteLine("Client Connection Success [ip]: " + tcp_socket.RemoteEndPoint.ToString() +" time: " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));

                        try
                        {
                            Byte[] received_byte = new Byte[1024];

                            tcp_socket.Receive(received_byte);

                            CPacket receivedPacket = Utility.func_ReadJson(received_byte);
                            Utility.func_DisplayPacketInfo(receivedPacket);
                            ValueKind value = ValueKind.NONE;

                            if (receivedPacket._newSequence.Length == 0 || receivedPacket._kind == CPacket.Kind.NONE)
                            {
                                Console.WriteLine("[ip]: " + tcp_socket.RemoteEndPoint.ToString() + " Send Non-meaninful Data");
                                Console.WriteLine("[ip] :" + tcp_socket.RemoteEndPoint.ToString() + "'s Connection is Finished");
                                tcp_socket.Close();
                                break;
                            }

                            if (this.func_IsTrain(receivedPacket) == true)
                            {
                                this._cMongo.func_insertTraingset(receivedPacket);
                                this._cmm.func_addTrainingSet((int)receivedPacket._value, receivedPacket._newSequence);
                                this._cmm.func_train();
                            }
                            else if (this.func_IsRequest(receivedPacket) == true)
                            {
                                Console.WriteLine("Now Starting Analysis from [ip] " + tcp_socket.RemoteEndPoint.ToString());
                                CHMMGenerator newCmm = new CHMMGenerator(8, 50);
                                
                                //value = this._cmm.func_analyze(receivedPacket._newSequence);
                                newCmm.func_train(this._cmm.func_getTrainingSet(), this._cmm.func_getOutputLabels());

                                value = (ValueKind)(newCmm.func_analyze(receivedPacket._newSequence));
                            }
                            CPacket returnPacket = new CPacket(CPacket.Kind.RETURN, value, receivedPacket._newSequence);


                            this.func_SendPacket2Client(returnPacket, tcp_socket);
                            

                        }
                        catch (NullReferenceException ne)
                        {
                            Console.WriteLine("[Exception] Null Packet occured");
                            int[] error = {-1};
                            CPacket returnPacket = new CPacket(CPacket.Kind.RETURN, (ValueKind)ValueKindMap.mapping(256), error);
                            this.func_SendPacket2Client(returnPacket, tcp_socket);
                        }
                    Console.WriteLine("[ip] : " + tcp_socket.RemoteEndPoint.ToString() + " 's Connection is Finished");
                    tcp_socket.Close();
                    Thread.Sleep(150);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("Client discconected from Server");
               
            }
          
        }

        private bool func_IsTrain(CPacket packet)
        {
            if (packet._kind == CPacket.Kind.TRAINING_SET)
                return true;

            return false;
        }

        private bool func_IsRequest(CPacket packet)
        {
            if (packet._kind == CPacket.Kind.REQUEST)
                return true;

            return false;
        }
        private string func_get_client_ip(TcpClient client)
        {
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            return ip;
        }

        private void func_DisplayPacketInfo(CPacket packet)
        {

            Console.WriteLine("kind: " + packet.func_GetKind() + "value: " + packet.func_GetValue() + "sequence: " + packet.func_GetSequence().ToString());
        }

        private void func_SendPacket2Client(CPacket packet, Socket sc)
        {
            string json = Utility.func_WriteJson(packet);
            byte[] sendbyte = Utility.func_Json2Byte(json);

            sc.Send(sendbyte, sendbyte.Length, 0);
        }

        private void func_InitializeTrainingSet() // DB에 저장된 트레이닝 셋을 초기화합니다.
        {
            
            List<CCollectionData> datalist = this._cMongo.func_getCollection();
            foreach (CCollectionData data in datalist)
            {
                this._cmm.func_addTrainingSet((int)data._valkind, data._sequence );
            }

            Console.Write("* Loading Traing set from Database.......... ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [SUCCESS]");
            Console.ResetColor();

        }


    }
}
