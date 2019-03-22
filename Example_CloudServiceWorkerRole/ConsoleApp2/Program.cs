using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener listener = new TcpListener(IPAddress.Any, 8086);
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
    }
}
