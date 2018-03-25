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
                Console.WriteLine("Nem adtal meg IP-cimet, kerlek probald ujra.");
            }
            else {
                IPAddress ipAddress;
                /**
                 * TryParse tries to parse the IP address given as the first argument
                 * the result is returned in the second argument
                 */

                if (IPAddress.TryParse(args[0], out ipAddress)) {
                    IPEndPoint endPointToSend= CreateIPEndPointFromString(args[0]);
                    udpClient.Send(udpDataToSend, 10, endPointToSend);
                }
                else Console.WriteLine();
                
            }
        }


        public static IPEndPoint CreateIPEndPointFromString(String endPointIPAddress) {
            if (endPointIPAddress == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                string[] endPointData = endPointIPAddress?.Split(':');

                //no port number is added
                if (endPointData.Length < 2)
                {
                    Console.WriteLine("Nem adtal meg port-szamot!");
                }

                //getting the IP address
                IPAddress IPAddress;
                if (!IPAddress.TryParse(endPointData[0], out IPAddress))
                {
                    Console.WriteLine("Hibas IP-cimet adtal meg!");
                }

                int port;
                if (!int.TryParse(endPointData[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
                {
                    Console.WriteLine("Portszam hiba!");
                }

                return new IPEndPoint(IPAddress, port);
            }
        }
    }
}
