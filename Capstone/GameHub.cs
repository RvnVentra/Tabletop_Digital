using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone
{
    public class GameHub : Hub
    {
        public async void Send(string input)
        {
            File.AppendAllText("Debug_Log.txt",
                DateTime.Now.ToString("MM-dd_HH-mm-ss") + "\t: " + input + "\n");

            await Clients.All.SendAsync("Receive", input);
        }
    }
}
