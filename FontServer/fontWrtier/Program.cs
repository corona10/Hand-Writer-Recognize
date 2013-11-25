using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fontWrtier
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClientModule test = new TcpClientModule("", 22);
            test.run();
        }
    }
}
