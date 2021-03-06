﻿using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace matrix4MuebUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient();
            byte[] udpDataToSend= new byte[4];
            Stopwatch stopWatch= new Stopwatch();
            double millisecondsElapsed;

            if (args.Length == 0)
            {
                Console.Error.WriteLine("Nem adtal meg IP-cimet, kerlek probald ujra!");
            }
            else {
                IPEndPoint endPointToSend;

                if (args[0].Equals("--help") || args[0].Equals("-h")) {
                    PrintHelp();
                }                
                else{

                    /* check if it is a valid IP address */
                    Boolean isIPAddressValid  = UDP.CheckIPAddressValidity(args[0]);

                    if (isIPAddressValid)
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
                                        Console.Error.WriteLine("Datagram elkuldve!");

					if(! Command.IfCommandReplies(args[1]) )
						System.Environment.Exit(1);
                                        stopWatch.Start();
                                        while (receivedDataSize == 0)
                                        {
                                            udpDataReceived = udpClient.Receive(ref endPointToSend);

                                            receivedDataSize = udpDataReceived.Length;
                                        }
                                        stopWatch.Stop();
                                        millisecondsElapsed = ((double)stopWatch.ElapsedTicks / Stopwatch.Frequency) * (double)1000;

                                        Console.Error.WriteLine("Erkezett adat!");
                                        Console.Error.WriteLine("Tartalma: " + Encoding.Default.GetString(udpDataReceived));
                                        Console.Error.WriteLine("Csomagkuldes ideje: " + string.Format("{0:0.000}", millisecondsElapsed) + " ms");
                                    }
                                    catch (SocketException se)
                                    {
                                        Console.Error.WriteLine("Nem elerheto socket.");
                                        Console.Error.WriteLine("SocketErrorCode: " + se.SocketErrorCode);
                                        Console.Error.WriteLine("ErrorCode: " + se.ErrorCode);
                                    }
                                }
                            }
                            else {
                                Console.Error.WriteLine("Ervenytelen parancs!");
                            }
                        }
                        else {
                            Console.Error.WriteLine("Nem adtal meg parancsot!");
                        }
                    }
                    else {
                        Console.Error.WriteLine("Ervenytelen IP-cimet adtal meg!");
                    }
                }                
            }
        }

        /* Prints help for the program */
        public static void PrintHelp()
        {
            Console.Error.WriteLine("Matrix 4 utility program");
            Console.Error.WriteLine("Hasznalhato parancsok: ");

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
