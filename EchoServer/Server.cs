using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer
{
    class Server
    {
        public void DoClient(TcpClient socket)
        {
            using (socket)
            {
                NetworkStream ns = socket.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);
                sw.AutoFlush = true;

                string line = sr.ReadLine();

                while (!string.IsNullOrEmpty(line))
                {
                    Console.WriteLine("Client: " + line);
                    sw.WriteLine(line.Length);
                    line = sr.ReadLine();
                }
            }
        }

        public TcpClient Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 7);
            listener.Start();
            Console.WriteLine("Server started");

            TcpClient socket = listener.AcceptTcpClient();

            Console.WriteLine("Server connected");

            return socket;
        }
    }
}
