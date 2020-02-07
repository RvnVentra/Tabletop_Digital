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
        readonly GameManager UM = GameManager.Instance;

        [HttpGet("[action]")]
        public Table GetBoard()
        {
            return UM.Table;
        }

        [HttpPost("DrawCard")]
        public Card DrawCard()
        {
            Card card = UM.DrawCard();
            UM.Table.Hand.Add(card);
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