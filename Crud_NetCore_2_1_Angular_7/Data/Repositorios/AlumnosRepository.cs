using Crud_NetCore_2_1_Angular_7.Data.Interfaces;
using Crud_NetCore_2_1_Angular_7.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_NetCore_2_1_Angular_7.Data.Repositorios
{
    public class AlumnosRepository : BaseRepository,IAlumnosRepository
    {
        public AlumnosRepository(string cnnString):base(cnnString)
        {   
        }
        public async Task<IEnumerable<Alumnos>> VerAlumnos()
        {
            using (IDbConnection db = GetConnection())
            {
                return await db.QueryAsync<Alumnos>("VerAlumnos", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
