﻿using System;
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
         * Checks if a number can be a valid IP port number.
         */
        public static Boolean CheckPortNumberValidity(String portNumberToCheck)
        {
            Int32 PortNumber;

            if (portNumberToCheck == null)
            {
                throw new NullReferenceException();                
            }
            else {
                //if port number is not too long
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
