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

            
                if (args[0].Contains(':')) {
                    endPointToSend = CreateIPEndPointFromString(args[0]);
                    int receivedDataSize = 0;
                    byte[] udpDataReceived = new byte[20];

                    if (endPointToSend != null)
                    {
                        try
                        {   /*
                            TODO: 
                            SocketException lekezelese
                            IP address validity check
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
                        catch (SocketException se) {
                            Console.WriteLine(se.SocketErrorCode);
                            Console.WriteLine(se.ErrorCode);
                        }

                    }
                }                
                else {
                    Console.WriteLine("Ervenytelen IP cimet adtal meg!");
                    Console.WriteLine("Kivant formatum: <IP-cim>:<port>"); 
                }
            }
        }


        public static IPEndPoint CreateIPEndPointFromString(String endPointIPAddress) {
            if (endPointIPAddress == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                string[] endPointAddressParts = endPointIPAddress.Split(':');

                //no port number is added
                if (endPointAddressParts.Length < 2)
                {
                    Console.WriteLine("Nem adtal meg port-szamot!");
                }

                //getting the IP address
                IPAddress IPAddress;
                if (!IPAddress.TryParse(endPointAddressParts[0], out IPAddress))
                {
                    Console.WriteLine("Hibas IP-cimet adtal meg!");
                }

                //getting the port number
                int port;
                if (!int.TryParse(endPointAddressParts[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
                {
                    Console.WriteLine("Portszam hiba!");
                }

                return new IPEndPoint(IPAddress, port);
            }
        }
    }
}
