using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.24.186"), 1337);
            listener.Start();
            Console.WriteLine("Server started");
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Server connected to a client");
                Task.Run(() =>
                {
                    TcpClient tempsocket = socket;
                    DoClient(tempsocket);
                });
            }
        }
    }
}
