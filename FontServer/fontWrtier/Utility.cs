using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fontWriter
{
    static class Utility
    {
        static public byte[] func_WriteBson(CPacket packet)
        {
            MemoryStream _ms = new MemoryStream();
            JsonSerializer _serializer = new JsonSerializer();
            BsonWriter writer = new BsonWriter(_ms);
            _serializer.Serialize(writer, packet);
            _ms.Seek(0, SeekOrigin.Begin);

            byte[] bson = _ms.ToArray();

            return bson;
        }

        static public string func_WriteJson(CPacket packet)
        {
            string json = " ";
            json = JsonConvert.SerializeObject(packet);
            return json;

        }

        static public byte[] func_Json2Byte(string json)
        {
            return System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
        }
        static public CPacket func_ReadJson(byte[] receivedData)
        {
            String json = System.Text.Encoding.UTF8.GetString(receivedData);
            CPacket packet = JsonConvert.DeserializeObject<CPacket>(json);

            return packet;
        }
        static public void func_DisplayPacketInfo(CPacket packet)
        {
            try
            {
                Console.WriteLine("kind: " + packet.func_GetKind() + " value: " + packet.func_GetValue().ToString() + " sequence 길이: " + packet.func_GetSequence().Length);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine("잘못된 패킷");
            }
        }

        static public CPacket func_decode_packet(byte[] bson)
        {
            MemoryStream _ms = new MemoryStream();
            BsonReader reader = new BsonReader(_ms);
            JsonSerializer _serializer = new JsonSerializer();
            CPacket decode = _serializer.Deserialize<CPacket>(reader);


            return decode;
        }
        
    }
}
