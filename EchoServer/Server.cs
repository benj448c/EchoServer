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

                    string[] str = line.Split(' ');
                    double result = 0;

                    if (str[0] == "add" || str[1] == "+" || str[0] == "sub" || str[1] == "-" || str[0] == "mul" || str[1] == "*" || str[0] == "div" || str[1] == "/")
                    {
                        result = Calculator(str);
                    }

                    sw.WriteLine(result);
                    line = sr.ReadLine();
                }

            }
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 3001);
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

        public double Calculator(string[] str)
        {
            double result = 0;

            if (str[0] == "add")
            {
                int a = Int32.Parse(str[1]);
                int b = Int32.Parse(str[2]);
                result = a + b;
            }
            if (str[1] == "+")
            {
                int a = Int32.Parse(str[0]);
                int b = Int32.Parse(str[2]);
                result = a + b;
            }

            if (str[0] == "sub")
            {
                int a = Int32.Parse(str[1]);
                int b = Int32.Parse(str[2]);
                result = a - b;
            }
            if (str[1] == "-")
            {
                int a = Int32.Parse(str[0]);
                int b = Int32.Parse(str[2]);
                result = a - b;
            }
            if (str[0] == "mul")
            {
                int a = Int32.Parse(str[1]);
                int b = Int32.Parse(str[2]);
                result = a * b;
            }
            if (str[1] == "*")
            {
                int a = Int32.Parse(str[0]);
                int b = Int32.Parse(str[2]);
                result = a * b;
            }
            if (str[0] == "div")
            {
                int a = Int32.Parse(str[1]);
                int b = Int32.Parse(str[2]);
                result = a / b;
            }
            if (str[1] == "/")
            {
                int a = Int32.Parse(str[0]);
                int b = Int32.Parse(str[2]);
                result = a / b;
            }

            return result;
        }
    }
}
