using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        readonly GameManager GM = GameManager.Instance;

        [HttpGet("CreateGame")]
        public string CreateGame()
        {
            string gameCode = GM.CreateGame();
            Debug.Log("Added Game:" + gameCode);

            return gameCode;
        }

        [HttpGet("JoinGame")]
        public bool JoinGame(string roomCode)
        {
            return GM.Games.ContainsKey(roomCode);
        }
    }
}