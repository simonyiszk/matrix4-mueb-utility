using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace udpTest
{
    class UDP
    {
        /*
         * Checks if an address is a valid IP address.
         */
        public static Boolean CheckIPAddressValidity(String addressToCheck)
        {
            string[] IPstring= addressToCheck.Split('.');            

            int temp;
            Boolean isValid= true;

            for (int i = 0; i < 4; i++)
            {
                if (Int32.TryParse(IPstring[i], out temp))
                {
                    if (temp > 255 || temp < 0)
                    {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            return isValid;
        }

        /*
         * Checks if a number can be a valid IP port number.
         */
        public static Boolean CheckPortNumberValidity(String portNumberToCheck) {
            Int32 PortNumber;

            if (portNumberToCheck.Length < 6)
            {
                if (Int32.TryParse(portNumberToCheck, out PortNumber))
                {
                    if (PortNumber < 0 || PortNumber > 65535)
                    {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        
        public static IPEndPoint CreateIPEndPointFromString(String endPointIPAddress)
        {
            if (endPointIPAddress == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                string[] endPointAddressParts = endPointIPAddress.Split(':');

                //getting the IP address
                IPAddress iPAddress = IPAddress.Parse(endPointAddressParts[0]);

                //getting the port number
                Int32 portNumber= Int32.Parse(endPointAddressParts[1]);               

                return new IPEndPoint(iPAddress, portNumber);
            }
        }

        /* 
         * Checks if a given command is one of the valid commands 
         */
        public static Boolean CheckCommandValidity(String commandToCheck) {
            if (commandToCheck.Equals("12V-on") || commandToCheck.Equals("12V-off") ||
                commandToCheck.Equals("reboot") || commandToCheck.Equals("get-status") ||
                commandToCheck.Equals("start-animation") || commandToCheck.Equals("stop-animation")) {
                return true;
            }
            return false;
        }

        /* 
         * Returns the byte value representing a certain command 
         */
        public static byte commandAsByte(string commandToCast) {
            byte commandCode= 0;

            switch (commandToCast) {
                case "12V-off-left":
                    commandCode = 0;
                    break;
                case "12V-off-right":
                    commandCode = 1;
                    break;
                case "reset-left-panel":
                    commandCode = 2;
                    break;
                case "reset-right-panel":
                    commandCode = 3;
                    break;
                case "reboot":
                    commandCode = 4;
                    break;
                case "get-status":
                    commandCode = 5;
                    break;
                case "use-internal-animation":
                    commandCode = 10;
                    break;
                case "use-external-animation":
                    commandCode = 20;
                    break;
                case "blank":
                    commandCode = 30;
                    break;
                case "delete-anim-network-buffer":
                    commandCode = 6;
                    break;
                default:
                    break;
            }
            return commandCode;
        }
    }
}
