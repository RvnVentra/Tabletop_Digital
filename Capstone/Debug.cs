using System;
using System.Collections.Generic;
using System.IO;


namespace Capstone
{
    public class Debug
    {
        public static List<string> DebugLog = new List<string>();

        public static void Log(string input)
        {
            DebugLog.Add(DateTime.Now.ToString("MM-dd_HH-mm-ss") + ":\t" + input);

            System.Diagnostics.Debug.WriteLine(input);       
        }
    }
}
