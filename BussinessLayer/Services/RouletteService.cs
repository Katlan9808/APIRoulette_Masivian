using Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork.Interfaces;

namespace BussinessLayer.Services
{
    public interface IRouletteService
    {
        RouletteModel Create(RouletteModel Obj);
        RouletteModel Delete(int Id);
        IEnumerable<RouletteModel> Get();
        IEnumerable<RouletteModel> GetById(int Id);
        RouletteModel Update(RouletteModel obj);
        RouletteModel OpenRoulette(int Id);
    }
    public class RouletteService : IRouletteService
    {
        private IUnitOfWork _unitOfWork;

        public RouletteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public RouletteModel Create(RouletteModel Obj)
        {
            RouletteModel objResponse = new RouletteModel();
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    objResponse = context.Repositories.RouletteRepository.Create(Obj);
                    context.SaveChanges();

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RouletteModel Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RouletteModel> Get()
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var lstRoulette = context.Repositories.RouletteRepository.Get();
                    return lstRoulette;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RouletteModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public RouletteModel OpenRoulette(int Id)
        {
            RouletteModel objResponse = new RouletteModel();
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    objResponse = context.Repositories.RouletteRepository.OpenRoulette(Id);
                    context.SaveChanges();

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RouletteModel Update(RouletteModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
