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
            Tcp_Module tcp_module = new Tcp_Module(8000);
        }
    }
}
