using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerUtility
{
    static class Utility
    {
        static private byte[] func_WriteBson(Packet packet)
        {
            MemoryStream _ms = new MemoryStream();
            JsonSerializer _serializer = new JsonSerializer();
            BsonWriter writer = new BsonWriter(_ms);
            _serializer.Serialize(writer, packet);
            _ms.Seek(0, SeekOrigin.Begin);

            byte[] bson = _ms.ToArray();

            return bson;
        }


        static public Packet func_decode_packet(byte[] bson)
        {
            MemoryStream _ms = new MemoryStream();
            BsonReader reader = new BsonReader(_ms);
            JsonSerializer _serializer = new JsonSerializer();
            Packet decode = _serializer.Deserialize<Packet>(reader);


            return decode;
        }

        static public string func_WriteJson(Packet packet)
        {
            string json = JsonConvert.SerializeObject(packet);

            return json;

        }

        static public Packet func_ReadJson(byte[] receivedData)
        {
            String json = System.Text.Encoding.UTF8.GetString(receivedData);
            Packet packet = JsonConvert.DeserializeObject<Packet>(json);

            return packet;
        }

        static public byte[] func_Json2Byte(string json)
        {
            return System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
        }
        static public void func_DisplayPacketInfo(Packet packet)
        {

            Console.WriteLine("kind: " + packet.func_GetKind() + " value: " + packet.func_GetValue().ToString() + " sequence 길이: " + packet.func_GetSequence().Length);
        }
    }
}
