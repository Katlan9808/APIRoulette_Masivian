using Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork.Interfaces;

namespace BussinessLayer.Services
{
    public interface IClientService
    {
        ClientModel Create(ClientModel Obj);
        ClientModel Delete(int Id);
        IEnumerable<ClientModel> Get();
        IEnumerable<ClientModel> GetById(int Id);
        ClientModel Update(ClientModel obj);
    }
    public class ClientService : IClientService
    {

        private IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ClientModel Create(ClientModel Obj)
        {
            throw new NotImplementedException();
        }

        public ClientModel Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientModel> Get()
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var lstClients = context.Repositories.ClientRepository.Get();
                    return lstClients;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ClientModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ClientModel Update(ClientModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
