using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RunMyServer();
        }

        private void RunMyServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            while (true)
            {
                using (TcpClient client = listener.AcceptTcpClient())
                {
                    using (Stream stream = client.GetStream())
                    {
                        byte[] requestBuffer = new byte[100];
                        int size = stream.Read(requestBuffer, 0, requestBuffer.Length);
                        string msg = Encoding.ASCII.GetString(requestBuffer, 0, size);
                        Console.WriteLine(msg);
                        StreamWriter sw = new StreamWriter(stream);
                        sw.WriteLine($"It's {DateTime.Now}");
                        sw.Flush();
                    }

                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
