using System;
using System.IO;

namespace Capstone
{
    public static class Debug
    {
        //static readonly string FILE_PATH_MAIN =
        //    "bin\\Logs\\Debug_Log.txt";

        //static readonly string FILE_PATH_DATE =
        //    "bin\\Logs\\Debug_Log" + DateTime.Now.ToString("_MM-dd_HH-mm-ss") + ".txt";

        public static void Log(string input)
        {
            //File.AppendAllText(FILE_PATH_MAIN,
            //    DateTime.Now.ToString("MM-dd_HH-mm-ss") + "\t: " + input + "\n");

            //File.AppendAllText(FILE_PATH_DATE,
            //    DateTime.Now.ToString("MM-dd_HH-mm-ss") + "\t: " + input + "\n");

            System.Diagnostics.Debug.WriteLine(input);
        }
    }
}
