using DataLayer.Actions;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository.Interfaces
{
    public interface IBetRepository : ICreate<BetModel>, IUpdate<BetModel>, IRead<BetModel>, IDelete<BetModel>
    {
        IEnumerable<BetModel> CloseRoulette(ResponseBetModel objBetResponse);
        BetModel CreateBet(BetModel obBjet);

    }
}
