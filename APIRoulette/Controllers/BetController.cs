using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utilities;
using Utilities.BetUtilities;

namespace APIRoulette.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BetController : Controller
    {
        private readonly IBetService _BetService;
        public BetController(IBetService BetService)
        {
            _BetService = BetService;
        }


        [HttpPost("CreateBet")]
        public ActionResult<IEnumerable<string>> CreateBet(BetModel objBet)
        {
            try
            {
                ResponseAPI<BetModel> response = null;
                var user = Request.Headers["user_autenticated"].ToString();

                if (user != string.Empty)
                {
                    BetModel objOk = BetUtilities.ValidateBet(objBet);
                    response = new ResponseAPI<BetModel>(Parameters.OK_REQUEST, "OK", _BetService.CreateBet(objOk));
                }
                else
                {
                    response = new ResponseAPI<BetModel>(Parameters.BAD_REQUEST, "Missig user_autenticated", false);
                }


                return Ok(
                    response
            );
            }
            catch (Exception ex)
            {
                ResponseAPI<BetModel> response = new ResponseAPI<BetModel>(Parameters.INTERNAL_SERVER_ERROR, ex.Message, false);

                return Ok(
                    response
                    );
            }
        }
    }
}
