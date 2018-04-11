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
                                else Console.WriteLine("Ervenytelen parancs!");
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
    }
}
