using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.SQL
{
    public abstract class Command
    {
        protected SqlConnection _context;
        protected SqlTransaction _transaction;

        protected SqlCommand CrearComando(string query)
        {
            return new SqlCommand(query, _context, _transaction);
        }
    }
}
