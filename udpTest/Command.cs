using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace udpTest
{
    public class Command 
    {
        public static Tuple<string, byte, string>[] commands= {
            Tuple.Create<string, byte, string>("12V-off-left", 0, "12V kikapcsolasa a bal oldali tablan"),
            Tuple.Create<string, byte, string>("12V-off-right", 1, "12V kikapcsolasa a jobb oldali tablan"),
            Tuple.Create<string, byte, string>("reset-left-panel", 2, "bal oldali panel ujrainditasa"),
            Tuple.Create<string, byte, string>("reset-right-panel", 3, "jobb oldali panel ujrainditasa"),
            Tuple.Create<string, byte, string>("reboot", 4, "MUEB ujrainditasa"),
            Tuple.Create<string, byte, string>("get-status", 5, "statusz lekerdezese"),
            Tuple.Create<string, byte, string>("use-internal-animation", 10, "belso animacio hasznalata"),
            Tuple.Create<string, byte, string>("use-external-animation", 20, "kulso animacio hasznalata"),
            Tuple.Create<string, byte, string>("blank", 30, "tablak elsotetitese"),
            Tuple.Create<string, byte, string>("delete-anim-network-buffer", 6, "buffer torlese")
        };

        /* 
         * Returns the byte value representing a certain command 
         */
        public static byte CommandAsByte(string commandToCast)
        {
            byte commandCode = 0;

            for (int i = 0; i < commands.Length; i++) {
                if (commands[i].Item1.Equals(commandToCast)) {
                    commandCode = commands[i].Item2;
                }
            }           

            return commandCode;
        }

        /* 
         * Checks if a given command is one of the valid commands 
         */
        public static Boolean CheckCommandValidity(string commandToCheck)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                if (commandToCheck.Equals(commands[i].Item1))
                {
                    return true;
                }
            }
            return false;
        }
    };
}
