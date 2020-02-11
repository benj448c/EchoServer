using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
{
    class Client
    {
        public void Start()
        {
            TcpClient socket = new TcpClient("localhost", 7);

            using (socket)
            {
                NetworkStream ns = socket.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);
                sw.AutoFlush = true;

                bool loopexiter = true;

                while (loopexiter)
                {
                    string message = Console.ReadLine();
                    if (message == "//exit")
                    {
                        loopexiter = false;
                        continue;
                    }

                    sw.WriteLine(message);

                    string ServerAnswer = sr.ReadLine();

                    Console.WriteLine("Console: " + ServerAnswer);
                }
            }
        }
    }
}
