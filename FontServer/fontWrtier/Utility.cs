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
