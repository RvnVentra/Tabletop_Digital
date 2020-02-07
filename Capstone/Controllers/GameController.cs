using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        readonly GameManager GM = GameManager.Instance;

        [HttpGet("[action]")]
        public Table GetBoard()
        {
            return GM.Table;
        }

        [HttpPost("DrawCard")]
        public Card DrawCard()
        {
            Card card = GM.DrawCard();
            GM.Table.Hand.Add(card);
            Debug.Log("Card " + card.Color + ", " + card.Number + " Was Drawn");

            return card;
        }

        [HttpPost("PlayCard")]
        public bool PlayCard([Bind("cardID")] int cardID)
        {
            Debug.Log(cardID + " Was Played");
            return false;
        }

        public void CreateCookie()
        {
            Response.Cookies.Append("artistID", "10");     
        }
    }
}