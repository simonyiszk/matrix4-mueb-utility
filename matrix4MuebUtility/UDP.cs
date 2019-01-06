using System;
using System.Net;

namespace matrix4MuebUtility
{
    class UDP
    {
        /*
         * Checks if an address is a valid IP address.
         */
        public static Boolean CheckIPAddressValidity(String addressToCheck)
        {
            string[] IPparts = addressToCheck.Split('.');

            int temp;
            Boolean isValid = true;

            if (addressToCheck == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                if (IPparts.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Int32.TryParse(IPparts[i], out temp))
                        {
                            if (temp > 255 || temp < 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        /*
         * Creates an IPEndPoint from a given string containing the endpoint's address           
         */
        public static IPEndPoint CreateIPEndPointFromString(String endPointIPAddress)
        {
            if (endPointIPAddress == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                IPAddress iPAddress = IPAddress.Parse(endPointIPAddress);

                Int32 portNumber = 2000;

                return new IPEndPoint(iPAddress, portNumber);
            }
        }
    }    
}
