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
    public class RouletteRepository : Command, IRouletteRepository
    {
        public RouletteRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public RouletteModel Create(RouletteModel obj)
        {
            RouletteModel objResponse = new RouletteModel();
            try
            {
                var command = CrearComando("spCreateRoulette");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@ROULETTE_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@ROULETTE_CODE", SqlDbType.VarChar).Value = Guid.NewGuid().ToString();
                int ok = command.ExecuteNonQuery();
                if (ok > 0)
                {
                    int IdRoulette = int.Parse(command.Parameters["@ROULETTE_ID"].Value.ToString());
                    objResponse = GetById(IdRoulette);
                }

                return objResponse;
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
            List<RouletteModel> lstRoulette = new List<RouletteModel>(); ;
            try
            {
                var command = CrearComando("spGetRoulettes");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            RouletteModel objRoulette = SetRoulette(item);
                            lstRoulette.Add(objRoulette);
                        }
                    }

                    return lstRoulette;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RouletteModel GetById(int Id)
        {
            RouletteModel objRoulette = new RouletteModel(); ;
            try
            {
                var command = CrearComando("spGetRouletteById");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@ROULETTE_ID", SqlDbType.Int).Value = Id;
                using (var reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            objRoulette = SetRoulette(item);
                        }
                    }

                    return objRoulette;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RouletteModel OpenRoulette(int iIdRoulette)
        {
            RouletteModel objResponse = new RouletteModel();
            try
            {
                var command = CrearComando("spOpenRoulette");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@ROULETTE_ID", SqlDbType.Int).Value = iIdRoulette;

                int ok = command.ExecuteNonQuery();
                if (ok > 0)
                {
                    int IdRoulette = int.Parse(command.Parameters["@ROULETTE_ID"].Value.ToString());
                    objResponse = GetById(IdRoulette);
                }

                return objResponse;
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

        private RouletteModel SetRoulette(DataRow row)
        {
            RouletteModel objResponse = new RouletteModel();
            try
            {
                objResponse.iRouletteId = Convert.ToInt32(row["ROULETTE_ID"].ToString());
                objResponse.strRouletteCode = row["ROULETTE_CODE"].ToString();
                objResponse.strRouletteStatus = row["ROULETTE_STATUS"].ToString();
                if (row["OPENING_DATE"].ToString() != string.Empty)
                {
                    objResponse.dttOpeningDate = DateTime.Parse(row["OPENING_DATE"].ToString());
                }
                if (row["CLOSING_DATE"].ToString() != string.Empty)
                {
                    objResponse.dttClosingDate = DateTime.Parse(row["CLOSING_DATE"].ToString());
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
