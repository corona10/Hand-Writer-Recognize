using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Utility.func_readSpray();
            if(args.Length == 0)
            {
                Console.WriteLine("포트를 입력해주세요:");
                    return;
            }
            int port = Convert.ToInt32(args[0]);
            Console.WriteLine("* Server is Now Starting with Port: " + port);
            Tcp_Module tcp_module = new Tcp_Module(port);
        }
    }
}
