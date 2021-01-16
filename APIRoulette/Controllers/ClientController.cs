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
    public class ClientController : Controller
    {
        private readonly IClientService _ClientService;
        public ClientController(IClientService ClientService)
        {
            _ClientService = ClientService;
        }

        [HttpGet("GetClients")]
        public ActionResult<IEnumerable<string>> GetClients()
        {
            try
            {
                ResponseAPI<ClientModel> response = new ResponseAPI<ClientModel>(Parameters.OK_REQUEST, "OK", _ClientService.Get());

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
