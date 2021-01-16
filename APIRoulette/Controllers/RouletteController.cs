using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utilities;

namespace APIRoulette.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : Controller
    {
        private readonly IRouletteService _RouletteService;
        private readonly IBetService _BetService;
        public RouletteController(IRouletteService RouletteService, IBetService BetService)
        {
            _RouletteService = RouletteService;
            _BetService = BetService;
        }


        [HttpGet("GetRoulettes")]
        public ActionResult<IEnumerable<string>> GetRoulettes()
        {
            try
            {
                ResponseAPI<RouletteModel> response = new ResponseAPI<RouletteModel>("200", "Success", _RouletteService.Get());

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


        [HttpGet("CreateRoulette")]
        public ActionResult<IEnumerable<string>> CreateRoulette()
        {
            try
            {
                RouletteModel objRoulette = new RouletteModel();
                ResponseAPI<RouletteModel> response = new ResponseAPI<RouletteModel>(Parameters.OK_REQUEST, "Success", _RouletteService.Create(objRoulette));

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


        [HttpGet("OpenRoulette")]
        public ActionResult<IEnumerable<string>> OpenRoulette(int iIdRoulette)
        {
            try
            {
                ResponseAPI<RouletteModel> response = new ResponseAPI<RouletteModel>(Parameters.OK_REQUEST, "Success", _RouletteService.OpenRoulette(iIdRoulette));

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

        [HttpPost("CloseRoulette")]
        public ActionResult<IEnumerable<string>> CloseRoulette(ResponseBetModel objBet)
        {
            try
            {
                ResponseAPI<BetModel> response = new ResponseAPI<BetModel>(Parameters.OK_REQUEST, "Success", _BetService.CloseRoulette(objBet));
                var Winners = response.lstResponseList.Where(a => a.objResponseBetModel.iIdRoulette == -999).FirstOrDefault();
                if (Winners != null)
                {
                    response.strResponseMessage += " No hubo ganadores !!";
                }
                else
                {
                    response.strResponseMessage += " Los ganadores son !!";
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
