using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_NetCore_2_1_Angular_7.Data.Repositorios
{
    public class BaseRepository
    {
        private readonly string cnnString;
        public BaseRepository(string cnnString)
        {
            this.cnnString = cnnString;
        }
        public IDbConnection GetConnection()
        {
            return new SqlConnection(cnnString);
        } 
    }
}
