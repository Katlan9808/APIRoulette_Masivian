using DataLayer.Repository.Interfaces;
using DataLayer.SQL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.Repository.Implementations
{
    public class BetRepository : Command, IBetRepository
    {
        public BetRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public void BetWinners(List<BetModel> lstBetWinners)
        {
            try
            {
                foreach (var item in lstBetWinners)
                {

                    if (item.strBetColor == item.objResponseBetModel.strWinningColor && item.iBetNumber == item.objResponseBetModel.iWinningNumber)
                    {
                        item.lgBetValue = item.lgBetValue * 5;
                    }
                    else if (item.strBetColor == item.objResponseBetModel.strWinningColor)
                    {
                        item.lgBetValue = (item.lgBetValue * 1.8) + item.lgBetValue;
                    }

                    var command = CrearComando("spAwardWinners");
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@CLIENT_ID", SqlDbType.Int).Value = item.iFkClientId;
                    command.Parameters.Add("@BET_VALUE", SqlDbType.Decimal).Value = item.lgBetValue;
                    int ok = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ParticipateBet(ClientModel objClient)
        {
            try
            {
                var command = CrearComando("spParticipateBet");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@CLIENT_ID", SqlDbType.Int).Value = objClient.iClientId;
                command.Parameters.Add("@BET_VALUE", SqlDbType.Decimal).Value = objClient.lgClientMoney;
                int ok = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BetModel> CloseRoulette(ResponseBetModel objBetResponse)
        {
            List<BetModel> lstResponse = new List<BetModel>();

            try
            {
                var command = CrearComando("spCloseRoulette");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@ROULETTE_ID", SqlDbType.Int).Value = objBetResponse.iIdRoulette;
                command.Parameters.Add("@WINNING_NUMBER", SqlDbType.Int).Value = objBetResponse.iWinningNumber;
                command.Parameters.Add("@WINNING_COLOR", SqlDbType.VarChar).Value = objBetResponse.strWinningColor;
                using (var reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            BetModel objBet = new BetModel();
                            objBet.iFkClientId = int.Parse(item["FK_CLIENT_ID"].ToString());
                            objBet.lgBetValue = double.Parse(item["BET_VALUE"].ToString());
                            objBet.strBetColor = item["BET_COLOR"].ToString();
                            objBet.iBetNumber = int.Parse(item["BET_NUMBER"].ToString());
                            objBet.objResponseBetModel.iWinningNumber = objBetResponse.iWinningNumber;
                            objBet.objResponseBetModel.strWinningColor = objBetResponse.strWinningColor;
                            lstResponse.Add(objBet);
                        }
                    }
                    else
                    {
                        BetModel objBet = new BetModel();
                        objBet.objResponseBetModel.iWinningNumber = objBetResponse.iWinningNumber;
                        objBet.objResponseBetModel.strWinningColor = objBetResponse.strWinningColor;
                        objBet.objResponseBetModel.iIdRoulette = -999;
                        lstResponse.Add(objBet);
                    }
                }

                var Winners = lstResponse.Find(a => a.objResponseBetModel.iIdRoulette == -999);
                if (Winners == null)
                {
                    BetWinners(lstResponse);
                }
                

                return lstResponse;
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
                var command = CrearComando("spCreateBet");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@BET_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@BET_CODE", SqlDbType.VarChar).Value = Guid.NewGuid().ToString();
                command.Parameters.Add("@BET_KEY", SqlDbType.VarChar).Value = Obj.strBetKey;
                command.Parameters.Add("@BET_VALUE", SqlDbType.Decimal).Value = Obj.lgBetValue;
                command.Parameters.Add("@FK_ID_ROULETTE_ID", SqlDbType.Int).Value = Obj.iFkRouletteId;
                command.Parameters.Add("@FK_CLIENT_ID", SqlDbType.Int).Value = Obj.iFkClientId;
                command.Parameters.Add("@BET_COLOR", SqlDbType.VarChar).Value = Obj.strBetColor;
                command.Parameters.Add("@BET_NUMBER", SqlDbType.Int).Value = Obj.iBetNumber;

                int ok = command.ExecuteNonQuery();
                if (ok > 0)
                {
                    int IdBet = int.Parse(command.Parameters["@BET_ID"].Value.ToString());
                    objResponse = GetById(IdBet);
                }

                ClientModel objClient = new ClientModel
                {
                    iClientId = Obj.iFkClientId,
                    lgClientMoney = Obj.lgBetValue
                };

                ParticipateBet(objClient);

                return objResponse;
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
                var command = CrearComando("spCreateBet");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@BET_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@BET_CODE", SqlDbType.VarChar).Value = Guid.NewGuid().ToString();
                command.Parameters.Add("@BET_KEY", SqlDbType.VarChar).Value = obBjet.strBetColor + ";" + obBjet.iBetNumber + ";" + obBjet.strBetKey;
                command.Parameters.Add("@BET_VALUE", SqlDbType.Decimal).Value = obBjet.lgBetValue;
                command.Parameters.Add("@FK_ID_ROULETTE_ID", SqlDbType.Int).Value = obBjet.iFkRouletteId;
                command.Parameters.Add("@FK_CLIENT_ID", SqlDbType.Int).Value = obBjet.iFkClientId;
                command.Parameters.Add("@BET_COLOR", SqlDbType.VarChar).Value = obBjet.strBetColor;
                command.Parameters.Add("@BET_NUMBER", SqlDbType.Int).Value = obBjet.iBetNumber;

                int ok = command.ExecuteNonQuery();
                if (ok > 0)
                {
                    int IdBet = int.Parse(command.Parameters["@BET_ID"].Value.ToString());
                    objResponse = GetById(IdBet);
                }

                ClientModel objClient = new ClientModel
                {
                    iClientId = obBjet.iFkClientId,
                    lgClientMoney = obBjet.lgBetValue
                };

                ParticipateBet(objClient);

                return objResponse;
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

        public BetModel GetById(int Id)
        {
            BetModel objBet = new BetModel(); ;
            try
            {
                var command = CrearComando("spGetBetById");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            objBet = SetBet(item);
                        }
                    }

                    return objBet;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BetModel Update(BetModel obj)
        {
            throw new NotImplementedException();
        }

        private BetModel SetBet(DataRow row)
        {
            BetModel objResponse = new BetModel();
            try
            {
                objResponse.iBetId = Convert.ToInt32(row["BET_ID"].ToString());
                objResponse.strBetCode = row["BET_CODE"].ToString();
                objResponse.strBetKey = row["BET_KEY"].ToString();
                objResponse.lgBetValue = double.Parse(row["BET_VALUE"].ToString());
                objResponse.dttBetDate = DateTime.Parse(row["BET_DATE"].ToString());
                objResponse.iFkRouletteId = int.Parse(row["FK_ROULETTE_ID"].ToString());
                objResponse.iFkClientId = int.Parse(row["FK_CLIENT_ID"].ToString());
                objResponse.strBetColor = row["BET_COLOR"].ToString();
                objResponse.iBetNumber = int.Parse(row["BET_NUMBER"].ToString());

                return objResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
