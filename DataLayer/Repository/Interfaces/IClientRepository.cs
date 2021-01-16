using DataLayer.Actions;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository.Interfaces
{
    public interface IClientRepository : ICreate<ClientModel>, IUpdate<ClientModel>, IRead<ClientModel>, IDelete<ClientModel>
    {
    }
}
