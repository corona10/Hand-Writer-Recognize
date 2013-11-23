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
        private static TcpListener _tcpListener;
        private CPacket _packet;
        private MemoryStream _ms;
        private JsonSerializer _serializer;
        private int tcp_port;
        private Thread _tcpThread;
        private Thread _dbThread;

        
        

        private string _db_url = "mongodb://168.188.111.43:21/test";
        private MongoClient _db_client;

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

                this._dbThread = new Thread(new ThreadStart(this.func_Connect2MongoDB));
                this._tcpThread = new Thread(new ThreadStart(this.tcp));
                Console.WriteLine("DB서버 접속 시도 중 입니다...");

                this._dbThread.Start();
                this._dbThread.Join();

                this._tcpThread.Start();
                Console.WriteLine("서버 초기화 성공");
                Console.WriteLine("포트: " + this.tcp_port);

            }
            catch (Exception e)
            {
                Console.WriteLine("서버 초기화 실패");
            }

        }

        public void func_Connect2MongoDB()
        {
            try
            {

                this._db_client = new MongoClient(this._db_url);
                _db_client.GetServer().Connect();
                Console.WriteLine("MongoDB 서버 버전 정보: " + this._db_client.GetServer().BuildInfo.VersionString);
                Console.WriteLine("MongoDB 서버 ip : " + this._db_url);
                Console.WriteLine("MongoDB 서버 연결 상태: " + this._db_client.GetServer().State);
                
                
                MongoDatabase testDb = this._db_client.GetServer().GetDatabase("test1");
                if(testDb.CollectionExists("test_collection") == false)
                    testDb.CreateCollection("test_collection");
                Console.WriteLine("-----------------------------------------------------");
            }
            catch (MongoAuthenticationException me)
            {
                Console.WriteLine("DB 서버 인증 실패");
            }
            catch (MongoConnectionException mc)
            {
                 Console.WriteLine("DB 서버 접속 실패");
                 Environment.Exit(0);
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
                    Console.WriteLine("클라이언트 연결 성공 [ip]: " + tcp_socket.RemoteEndPoint.ToString());
                    Byte[] received_byte = new Byte[1024];

                    tcp_socket.Receive(received_byte);
                    CPacket receivedPacket = Utility.func_ReadJson(received_byte);
                    Utility.func_DisplayPacketInfo(receivedPacket);
                    if (this.func_IsTrain(receivedPacket) == true)
                    {
                        this._cmm.func_addTrainingSet((int)receivedPacket._value, receivedPacket._newSequence);
                        this._cmm.func_train();
                    }else if(this.func_IsRequest(receivedPacket) == true)
                    {

                    }
                    CPacket returnPacket = new CPacket(CPacket.Kind.RETURN, 1, receivedPacket._newSequence);
                    
                    
                    this.func_SendPacket2Client(returnPacket, tcp_socket);
                    
                    Console.WriteLine("접속 종료");

                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("클라이언트가 접속을 끊었습니다");
                
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


    }
}
