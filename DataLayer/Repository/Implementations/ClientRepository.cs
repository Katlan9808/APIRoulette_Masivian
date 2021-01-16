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
    public class ClientRepository : Command, IClientRepository
    {

        public ClientRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
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
            List<ClientModel> lstClient = new List<ClientModel>(); ;
            try
            {
                var command = CrearComando("spGetClients");
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            ClientModel objClient = new ClientModel();
                            objClient.iClientId = Convert.ToInt32(item["CLIENT_ID"].ToString());
                            objClient.strClientCode = item["CLIENT_CODE"].ToString();
                            objClient.lgClientMoney = long.Parse(item["CLIENT_MONEY"].ToString());
                            objClient.strClientName = item["CLIENT_NAME"].ToString();
                            objClient.StrClientLastName = item["CLIENT_LASTNAME"].ToString();
                            lstClient.Add(objClient);
                        }
                    }
                    return lstClient;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClientModel GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ClientModel Update(ClientModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
