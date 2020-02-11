using System;
using System.Net.Sockets;

namespace EchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            TcpClient client = server.Start();
            server.DoClient(client);
        }
    }
}
