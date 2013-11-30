
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FontServer
{
    static class Utility
    {
        static private byte[] func_WriteBson(CPacket packet)
        {
            MemoryStream _ms = new MemoryStream();
            JsonSerializer _serializer = new JsonSerializer();
            BsonWriter writer = new BsonWriter(_ms);
            _serializer.Serialize(writer, packet);
            _ms.Seek(0, SeekOrigin.Begin);

            byte[] bson = _ms.ToArray();

            return bson;
        }


        static public CPacket func_decode_packet(byte[] bson)
        {
            MemoryStream _ms = new MemoryStream();
            BsonReader reader = new BsonReader(_ms);
            JsonSerializer _serializer = new JsonSerializer();
            CPacket decode = _serializer.Deserialize<CPacket>(reader);


            return decode;
        }

        static public string func_WriteJson(CPacket packet)
        {
            string json = JsonConvert.SerializeObject(packet);

            return json;

        }

        static public CPacket func_ReadJson(byte[] receivedData)
        {
            String json = System.Text.Encoding.UTF8.GetString(receivedData);
            CPacket packet = JsonConvert.DeserializeObject<CPacket>(json);

            return packet;
        }

        static public byte[] func_Json2Byte(string json)
        {
            return System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
        }
        static public void func_DisplayPacketInfo(CPacket packet)
        {

            Console.WriteLine("kind: " + packet.func_GetKind() + " value: " + packet.func_GetValue().ToString() + " sequence 길이: " + packet.func_GetSequence().Length);
        }

        static public void func_readSpray()
        {
            try
            {
                StreamReader sr = new StreamReader("spray.sp");


                String line;

                while ((line = sr.ReadLine()) != null)
                {

                    Console.WriteLine(line);

                }

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("------------------------Dev Team Information --------------------");
                Console.WriteLine("* Dong-hee, Na - Network Programmer & Lead Programmer");
                Console.WriteLine("* Sun-min, Kim,- Client Programmer with .NET");
                Console.WriteLine("* Joon-woo, Choi, - Algorithm Designer");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine();
            }
        }
    }
}
