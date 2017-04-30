using System;
using System.Collections.Generic;
using System.Text;

namespace F4CIOsDNSUpdaterCommon
{
    public class Constants
    {
        static string ProgramFolder {
			get
			{
				return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().CodeBase).ToLower().Replace("file:\\", string.Empty);
			}
		}
        const string IniFileName = "F4CIOsDNSUpdater.ini";
        const string LogFileName = "F4CIOsDNSUpdater.log";
        const string HtmFileName = "LastResponse.htm";

        public static string IniFilePath
        {
            get{
                return ProgramFolder+"\\"+IniFileName;
            }
        }

        public static string LogFilePath
        {
            get{
                return ProgramFolder + "\\" + LogFileName;
            }
        }

        public static string HtmFilePath
        {
            get
            {
                return ProgramFolder + "\\" + HtmFileName;
            }
        }
    }
}
