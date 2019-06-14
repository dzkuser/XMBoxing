using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.Comm;

namespace SocketStart
{
    class Start
    {

        static void Main(string[] args) {

           XMBOXING.Comm.SuperSocketHandler superSocketHandler = new XMBOXING.Comm.SuperSocketHandler();
            superSocketHandler.SetUp();
            Console.WriteLine("启动");
            Console.ReadLine();
           
        }
    }
}
