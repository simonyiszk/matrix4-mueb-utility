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
            string[] IPstring = addressToCheck.Split('.');

            int temp;
            Boolean isValid = true;

            for (int i = 0; i < 4; i++)
            {
                if (Int32.TryParse(IPstring[i], out temp))
                {
                    if (temp > 255 || temp < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return isValid;
        }

        /*
         * Checks if a number can be a valid IP port number.
         */
        public static Boolean CheckPortNumberValidity(String portNumberToCheck)
        {
            Int32 PortNumber;

            if (portNumberToCheck.Length < 6)
            {
                if (Int32.TryParse(portNumberToCheck, out PortNumber))
                {
                    if (PortNumber < 0 || PortNumber > 65535)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /**
            Creates an IPEndPoint from a given string containing the endpoint's address
            @param
             */
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
                Int32 portNumber = Int32.Parse(endPointAddressParts[1]);

                return new IPEndPoint(iPAddress, portNumber);
            }
        }
    }    
}
