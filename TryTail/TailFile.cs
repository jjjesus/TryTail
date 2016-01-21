using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace TryTail
{
    public class TailFile
    {
        public string FileName { get; set; }
        public string HostName { get; set; }
        public int PortNum { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private SshClient sshClient;
        private ShellStream shellStream;

        //Command being executed to tail a file.
        private readonly string command = "tail -f -n+1 {0}";

        //EventHandler that is called when new data is received.
        public EventHandler<string> DataReceived;

        public void Tail()
        {
            sshClient = new SshClient(HostName, PortNum, UserName, Password);
            sshClient.Connect();

            shellStream = sshClient.CreateShellStream("Tail", 0, 0, 0, 0, 1024);

            shellStream.DataReceived += (sender, dataEvent) =>
            {
                if (DataReceived != null)
                {
                    DataReceived(this, shellStream.Read());
                }
            };

            shellStream.WriteLine(string.Format(command, FileName));
        }
    }
}
