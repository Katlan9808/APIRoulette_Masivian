using Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork.Interfaces;
using Utilities;
using Utilities.BetUtilities;

namespace BussinessLayer.Services
{

    public interface IBetService
    {
        BetModel Create(BetModel Obj);
        BetModel Delete(int Id);
        IEnumerable<BetModel> Get();
        IEnumerable<BetModel> GetById(int Id);
        BetModel Update(BetModel obj);
        IEnumerable<BetModel> CloseRoulette(ResponseBetModel objBetResponse);
        BetModel CreateBet(BetModel obBjet);
    }

    public class BetService : IBetService
    {
        private IUnitOfWork _unitOfWork;

        public BetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BetModel> CloseRoulette(ResponseBetModel objBetResponse)
        {
            IEnumerable<BetModel> lstResponse = null;
            try
            {
                ResponseBetModel ValuesWinners = new ResponseBetModel();
                ValuesWinners = BetUtilities.CalculateWinnersBet();
                ValuesWinners.iIdRoulette = objBetResponse.iIdRoulette;
                using (var context = _unitOfWork.Create())
                {
                    lstResponse = context.Repositories.BetRepository.CloseRoulette(ValuesWinners);
                    context.SaveChanges();

                    return lstResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BetModel Create(BetModel Obj)
        {
            BetModel objResponse = new BetModel();
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    objResponse = context.Repositories.BetRepository.CreateBet(Obj);
                    context.SaveChanges();

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BetModel CreateBet(BetModel obBjet)
        {
             BetModel objResponse = new BetModel();
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    objResponse = context.Repositories.BetRepository.CreateBet(obBjet);
                    context.SaveChanges();

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BetModel Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BetModel> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BetModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public BetModel Update(BetModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
