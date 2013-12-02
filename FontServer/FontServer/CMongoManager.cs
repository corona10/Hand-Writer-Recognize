/**
 @section LICENSE
 
 The MIT License (MIT)

Copyright (c) 2013 Dong-hee,Na <corona10@gmail.com> 
                   Jun-woo, Choi <choigo92@gmail.com>  
                   Sun-min, Kim <mh5537@naver.com>

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
