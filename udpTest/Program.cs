using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace udpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient();
            byte[] udpDataToSend= new byte[4];
            Stopwatch stopWatch= new Stopwatch();
            double nanosecondsElapsed;
            double millisecondsElapsed;

            if (args.Length == 0)
            {
                Console.WriteLine("Nem adtal meg IP-cimet, kerlek probald ujra!");
            }
            else {
                IPEndPoint endPointToSend;
                if (args[0].Equals("--help") || args[0].Equals("-h")) {
                    PrintHelp();
                }
                /* check if address is given in correct format */
                else if (args[0].Contains(':')) {
                    string[] argumentsSplitted = args[0].Split(':');

                    /* check if it is a valid address */
                    Boolean isIPAddressValid  = UDP.CheckIPAddressValidity(argumentsSplitted[0]);
                    Boolean isPortNumberValid = UDP.CheckPortNumberValidity(argumentsSplitted[1]);

                    if (isIPAddressValid)
                    {
                        if (isPortNumberValid)
                        {
                            if (args.Length == 2)
                            {
                                Boolean isCommandValid = Command.CheckCommandValidity(args[1]);
                                if (isCommandValid)
                                {
                                    udpDataToSend[0] = (byte)'S';
                                    udpDataToSend[1] = (byte)'E';
                                    udpDataToSend[2] = (byte)'M';
                                    udpDataToSend[3] = Command.CommandToByte(args[1]);

                                    endPointToSend = UDP.CreateIPEndPointFromString(args[0]);
                                    int receivedDataSize = 0;
                                    byte[] udpDataReceived = new byte[20];

                                    if (endPointToSend != null)
                                    {
                                        try
                                        {
                                            /*
                                             * TODO 
                                             * SocketException kezelese normalisan
                                             */
                                            udpClient.Send(udpDataToSend, 4, endPointToSend);
                                            Console.WriteLine("Datagram elkuldve!");

                                            stopWatch.Start();
                                            while (receivedDataSize == 0)
                                            {
                                                udpDataReceived = udpClient.Receive(ref endPointToSend);

                                                receivedDataSize = udpDataReceived.Length;
                                            }
                                            stopWatch.Stop();
                                          //nanosecondsElapsed = ((double)stopWatch.ElapsedTicks / Stopwatch.Frequency) * (double)1000000000;
                                          //millisecondsElapsed = stopWatch.ElapsedMilliseconds;
                                            millisecondsElapsed = ((double)stopWatch.ElapsedTicks / Stopwatch.Frequency) * (double)1000;


                                            Console.WriteLine("Erkezett adat!");
                                            Console.WriteLine("Tartalma: " + Encoding.Default.GetString(udpDataReceived));
                                          //Console.WriteLine("Csomagkuldes ideje: " + string.Format("{0:0.000}", nanosecondsElapsed) + " ns");
                                            Console.WriteLine("Csomagkuldes ideje: " + string.Format("{0:0.000}", millisecondsElapsed) + " ms");

                                        }
                                        catch (SocketException se)
                                        {
                                            Console.WriteLine("Nem elerheto socket.");
                                            Console.WriteLine("SocketErrorCode: "+se.SocketErrorCode);
                                            Console.WriteLine("ErrorCode: "+se.ErrorCode);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ervenytelen parancs!");
                                }
                            }
                            else Console.WriteLine("Nem adtal meg parancsot!");
                        }
                        else {
                            Console.WriteLine("Ervenytelen port-szam!");
                        }
                    }
                    else {
                        Console.WriteLine("Ervenytelen IP-cimet adtal meg!");
                    }
                }                
                else {
                    Console.WriteLine("Ervenytelen cim-formatum!");
                    Console.WriteLine("Kivant formatum: <IP-cim>:<port>"); 
                }
            }
        }

        /* Prints help for the program */
        public static void PrintHelp()
        {
            Console.WriteLine("Matrix 4 tesztelo program");
            Console.WriteLine("Hasznalhato parancsok: ");

            int longestCommand= Command.commands[0].Item1.Length;
            for (int i = 1; i < Command.commands.Length; i++)
            {
                if (Command.commands[i].Item1.Length > longestCommand) {
                    longestCommand= Command.commands[i].Item1.Length;
                }
            }

            for (int i = 0; i < Command.commands.Length; i++) {
                Console.WriteLine(
                    String.Format(
                        "{0,-"+longestCommand+"}: {1}", 
                        Command.commands[i].Item1, 
                        Command.commands[i].Item3)
                );
            }
        }
    }
}