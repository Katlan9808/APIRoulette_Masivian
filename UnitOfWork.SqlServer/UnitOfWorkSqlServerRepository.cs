using DataLayer.Repository.Implementations;
using DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public IRouletteRepository RouletteRepository { get; }
        public IBetRepository BetRepository { get; }
        public IClientRepository ClientRepository { get; }

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            RouletteRepository = new RouletteRepository(context, transaction);
            BetRepository = new BetRepository(context, transaction);
            ClientRepository = new ClientRepository(context, transaction);
        }
    }
}
