using DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IClientRepository ClientRepository { get; }
        IBetRepository BetRepository { get; }
        IRouletteRepository RouletteRepository { get; }
    }
}
