using DataLayer.Actions;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository.Interfaces
{
    public interface IRouletteRepository : ICreate<RouletteModel>, IUpdate<RouletteModel>, IRead<RouletteModel>, IDelete<RouletteModel>
    {
        RouletteModel OpenRoulette(int Id);
    }
}
