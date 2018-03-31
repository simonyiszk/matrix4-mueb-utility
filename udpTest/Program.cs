using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace udpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient();
            byte[] udpDataToSend= { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            if (args.Length == 0)
            {
                Console.WriteLine("Nem adtal meg IP-cimet, kerlek probald ujra!");
            }
            else {
                IPEndPoint endPointToSend;

                /* check if address is given in correct format */
                if (args[0].Contains(':')) {
                    string[] argumentsSplitted = args[0].Split(':');

                    /* check if it is a valid address */
                    Boolean isIPAddressValid = UDP.CheckIPAddressValidity(argumentsSplitted[0]);
                    Boolean isPortNumberValid = UDP.CheckPortNumberValidity(argumentsSplitted[1]);               

                    if (isIPAddressValid)
                    {
                        if (isPortNumberValid)
                        {
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
                                    udpClient.Send(udpDataToSend, 10, endPointToSend);
                                    Console.WriteLine("Datagram elkuldve!");
                                    while (receivedDataSize == 0)
                                    {
                                        udpDataReceived = udpClient.Receive(ref endPointToSend);
                                        receivedDataSize = udpDataReceived.Length;
                                        Console.WriteLine("Erkezett adat!");
                                        Console.WriteLine("Tartalma: " + System.Text.Encoding.Default.GetString(udpDataReceived));
                                    }
                                }
                                catch (SocketException se)
                                {
                                    Console.WriteLine("Nem elerheto socket.");
                                    Console.WriteLine(se.SocketErrorCode);
                                    Console.WriteLine(se.ErrorCode);
                                }
                            }
                        } else Console.WriteLine("Ervenytelen port-szam!");
                    }
                    else Console.WriteLine("Ervenytelen IP-cimet adtal meg.");
                }                
                else {
                    Console.WriteLine("Ervenytelen cim-formatum!");
                    Console.WriteLine("Kivant formatum: <IP-cim>:<port>"); 
                }
            }
        }        
    }
}
