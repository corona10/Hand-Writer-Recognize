using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace FontServer
{
    class CMongoManager
    {
        private static CMongoManager instance = null;
        private static readonly object padlock = new object();
        private static string _db_url = "mongodb://localhost:21/test";
        private static MongoClient _db_client;
        private static MongoCollection<CCollectionData> _dbCollection;
        private static MongoDatabase _testDb;

        private CMongoManager()
        {
            
            Console.Write("* DataBase Manager Initialized.......... ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [SUCCESS]");
            Console.ResetColor();
        }

        public static CMongoManager func_Instance()
        {
            
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CMongoManager();
                    }
                    return instance;
                }
            
        }

     

         public void func_Connect2MongoDB()
        {
            try
            {
                if (_db_client == null)
                {
                    _db_client = new MongoClient(_db_url);
                    _db_client.GetServer().Connect();
                }
                Console.WriteLine("* MongoDB Version Info " + _db_client.GetServer().BuildInfo.VersionString);
                Console.WriteLine("* MongoDB Server IP Address : " + _db_url);
                Console.WriteLine("* MongoDB Server Connection Status: " + _db_client.GetServer().State);


                _testDb = _db_client.GetServer().GetDatabase("test1");
                if (_testDb.CollectionExists("test_collection") == false)
                {
                    _testDb.CreateCollection("test_collection");
                }
                else
                {
                    _dbCollection = _testDb.GetCollection<CCollectionData>("test_collection");
                }
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
         public List<CCollectionData> func_getCollection()
         {
             MongoCursor<CCollectionData> cursor = _dbCollection.FindAll();

             return cursor.ToList<CCollectionData>();

         }

         public void func_insertTraingset(CPacket packet)
        {
            try
            {
                CCollectionData data = new CCollectionData(packet._value, packet._newSequence);
                _dbCollection.Insert(data);
            }
            catch (MongoQueryException mq)
            {
                Console.WriteLine("[Error] Query Failed!!");
                Console.WriteLine("Packet Info: " + "Header: " + packet._kind + "Value: " + packet._value + "Sequence: " + packet._newSequence);
            }

        }
    }
}
