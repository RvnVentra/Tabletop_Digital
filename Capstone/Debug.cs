using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.SignalR;

namespace Capstone
{
    public static class Debug
    {
        //static readonly string FILE_PATH_MAIN =
        //    "bin\\Debug_Log.txt";

        public static List<string> DebugLog = new List<string>();

        //static readonly string FILE_PATH_DATE =
        //    "bin\\Debug_Log" + DateTime.Now.ToString("_MM-dd_HH-mm-ss") + ".txt";

        public static void Log(string input)
        {
            //File.AppendAllText(FILE_PATH_MAIN,
            //    DateTime.Now.ToString("MM-dd_HH-mm-ss") + ":\t" + input + "\n");

            //File.AppendAllText(FILE_PATH_DATE,
            //    DateTime.Now.ToString("MM-dd_HH-mm-ss") + ":\t" + input + "\n");

            DebugLog.Add(input);

            Console.WriteLine(input);

            System.Diagnostics.Debug.WriteLine(input);
        }
    }
}
