using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Tabletop.Models;

namespace Tabletop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        readonly GameManager GM = GameManager.Instance;

        private readonly TabletopContext _context;

        public MainController(TabletopContext context)
        {
            _context = context;
        }

        [HttpGet("AddAccount")]
        public void AddAccount(string username)
        {
            if (username != null)
                Debug.Log(username);
            else
                Debug.Log("NOPE");

            //foreach (string n in NameList.NameArray)
            //{
            //    _context.Account.Add(new Account
            //    {
            //        UserName = n,
            //        Password = "password",
            //        Email = n + "@mail.ca"
            //    });

            //    Debug.Log("ADD ACCOUNT: " + n);
            //}

            //_context.SaveChanges();
        }

        [HttpPost("test")]
        public IActionResult Test(string pl)
        {
            Debug.Log("TEST");
            Debug.Log(pl);

            if (pl == null)
            {
                return Ok();
            }

            PL plobj = JsonConvert.DeserializeObject<PL>(pl);

            Debug.Log(pl);
            Debug.Log(plobj.a);
            Debug.Log(plobj.b);



            return Ok($"Test Test a=={plobj.a} b=={plobj.b}");
        }

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

        [HttpGet("ClientId")]
        public string ClientId()
        {
            string id;

            if (Request.Cookies["ClientId"] == null)
            {
                id = Guid.NewGuid().ToString();
                Response.Cookies.Append("ClientId", id);
            }
            else
            {
                id = Request.Cookies["ClientId"];
            }

            Debug.Log(id);
            return id;
        }
    }

    public class PL
    {
        public string a { get; set; }
        public string b { get; set; }
    }
}