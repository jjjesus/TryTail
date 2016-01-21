using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryTail
{
    class Program
    {
        static void Main(string[] args)
        {
            TailFile tailFile = new TailFile()
            {
                HostName = "192.168.10.40",
                UserName = "root",
                Password = "root",
                PortNum = 22,
                FileName = "/root/workspace/oscar/TimerService.log"
            };
            tailFile.DataReceived = (sender, stringData) =>
                {
                    Console.WriteLine(stringData);
                };

            tailFile.Tail();

            Console.WriteLine("Hit ENTER to continue");
            Console.ReadLine();
        }
    }
}
