using System;

namespace matrix4MuebUtility
{
    
    public class Command 
    {
        /*
            Array containing the list of all commands
            First : command's name
            Second: command's value
            Third:  help to command
            Forth:  does the command get reply?
        */
        public static Tuple<string, int, string, Boolean>[] commands= {
            Tuple.Create("12V-off-left", 0x00, "12V kikapcsolasa a bal oldali tablan", false),
            Tuple.Create("12V-off-right", 0x01, "12V kikapcsolasa a jobb oldali tablan", false),
            Tuple.Create("reset-left-panel", 0x02, "bal oldali panel ujrainditasa", false),
            Tuple.Create("reset-right-panel", 0x03, "jobb oldali panel ujrainditasa", false),
            Tuple.Create("reboot", 0x04, "MUEB ujrainditasa", false),
            Tuple.Create("get-status", 0x05, "statusz lekerdezese", true),
            Tuple.Create("get-mac", 0x07, "MAC-cim lekerdezese", true),
            Tuple.Create("use-internal-animation", 0x10, "belso animacio hasznalata", false),
            Tuple.Create("use-external-animation", 0x20, "kulso animacio hasznalata", false),
            Tuple.Create("blank", 0x30, "tablak elsotetitese", false),
            Tuple.Create("delete-anim-network-buffer", 0x06, "halozati buffer torlese", false),
            Tuple.Create("ping", 0x40, "MUEB fociklus-idejenek meghatarozasa", true),
            Tuple.Create("enable-update", 0x50, "Frissites engedelyezese a MUEB-en", false),
	    Tuple.Create("get-new-fw-checksum", 0x51, "Uj FW checksumjanak lekerese", true),
	    Tuple.Create("refurbish", 0x60, "Uj FW beegetese", false),
	    Tuple.Create("swap", 0x70, "Ablakok csereje", false)
        };

        /* 
         * Returns the byte value representing a certain command 
         */
        public static byte CommandToByte(string commandToCast)
        {
            byte commandCode = 0;

            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i].Item1.Equals(commandToCast))
                {
                    commandCode =(byte)(commands[i].Item2);
                }
            }           

            return commandCode;
        }

	public static bool IfCommandReplies(string command)
	{
	for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i].Item1.Equals(command))
                {
                    return (bool)commands[i].Item4;
                }
            } 
	return false; //throw System.Exception("hiba"); TODO throw here
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
