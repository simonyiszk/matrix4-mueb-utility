using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace udpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient();
            byte[] udpDataToSend= new byte[4];

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
                    Boolean isIPAddressValid = UDP.CheckIPAddressValidity(argumentsSplitted[0]);
                    Boolean isPortNumberValid = UDP.CheckPortNumberValidity(argumentsSplitted[1]);                    

                    if (isIPAddressValid)
                    {
                        if (isPortNumberValid)
                        {
                            if (args.Length == 2)
                            {
                                Boolean isCommandValid = UDP.CheckCommandValidity(args[1]);
                                if (isCommandValid)
                                {
                                    udpDataToSend[0] = (byte)'S';
                                    udpDataToSend[1] = (byte)'E';
                                    udpDataToSend[2] = (byte)'M';
                                    udpDataToSend[3] = UDP.commandAsByte(args[1]);

                                    endPointToSend = UDP.CreateIPEndPointFromString(args[0]);
                                    int receivedDataSize = 0;
                                    byte[] udpDataReceived = new byte[20];

                                    if (endPointToSend != null)
                                    {
                                        try
                                        {
                                            /*
                                                TODO 
                                                SocketException kezelese normalisan
                                                */
                                            udpClient.Send(udpDataToSend, 4, endPointToSend);
                                            Console.WriteLine("Datagram elkuldve!");
                                            while (receivedDataSize == 0)
                                            {
                                                udpDataReceived = udpClient.Receive(ref endPointToSend);

                                                receivedDataSize = udpDataReceived.Length;
                                                Console.WriteLine("Erkezett adat!");
                                                Console.WriteLine("Tartalma: " + Encoding.Default.GetString(udpDataReceived));
                                            }
                                        }
                                        catch (SocketException se)
                                        {
                                            Console.WriteLine("Nem elerheto socket.");
                                            Console.WriteLine(se.SocketErrorCode);
                                            Console.WriteLine(se.ErrorCode);
                                        }
                                    }                                    
                                }
                                else {
                                    Console.WriteLine("Ervenytelen parancs!");                                    
                                }
                            } else Console.WriteLine("Nem adtal meg parancsot!");
                        } else Console.WriteLine("Ervenytelen port-szam!");
                    } else Console.WriteLine("Ervenytelen IP-cimet adtal meg!");
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
            Console.WriteLine("Hasznalhato parancsok: 12v-off, 12v-on, reboot, get-status, start-animation, stop-animation");
            Console.WriteLine("12v-on         : MUEB bekapcsolasa");
            Console.WriteLine("12v-off        : MUEB kikapcsolasa");
            Console.WriteLine("reboot         : MUEB ujrainditasa");
            Console.WriteLine("get-status     : MUEB statuszanak lekerdezese");
            Console.WriteLine("start-animation: animacio inditasa");
            Console.WriteLine("stop-animation : animacio megallitasa");

        }
    }
}


