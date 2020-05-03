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
    public class AlumnosRepository : BaseRepository, IAlumnosRepository
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
        public bool InsertarAlumno(Alumnos alumnos)
        {
            IDbTransaction dbTransaction;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", alumnos.Nombre);
            parameters.Add("@ApellidoPaterno", alumnos.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", alumnos.ApellidoMaterno);
            parameters.Add("@Carrera", alumnos.Carrera);

            using (IDbConnection db = GetConnection())
            {
                db.Open();
                dbTransaction = db.BeginTransaction();
                try
                {
                    db.ExecuteScalar("AlumnoInsert",commandType:CommandType.StoredProcedure, param:parameters,transaction:dbTransaction);
                    dbTransaction.Commit();
                    db.Close();
                    return true; 
                }
                catch (System.Exception)
                {
                    dbTransaction.Rollback();
                    db.Close();
                    return false;
                }
            }
        }
        public bool Eliminar(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@idAlumo", id);
            using (IDbConnection db = GetConnection())
            {
                db.Open();
                try
                {
                    db.ExecuteScalar("EliminarAlumno", commandType: CommandType.StoredProcedure, param: parameters);
                    db.Close();
                    return true;
                }
                catch (System.Exception)
                {
                    db.Close();
                    return false;
                }

            }
        }
        public bool EditarAlumno(int id, Alumnos alumnos)
        {
            IDbTransaction dbTransaction;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@idAlumo", id);
            parameters.Add("@Nombre", alumnos.Nombre);
            parameters.Add("@ApellidoPaterno", alumnos.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", alumnos.ApellidoMaterno);
            parameters.Add("@Carrera", alumnos.Carrera);

            using (IDbConnection db = GetConnection())
            {
                db.Open();
                dbTransaction = db.BeginTransaction();
                try
                {
                    db.ExecuteScalar("EditarAlumno_sp", commandType: CommandType.StoredProcedure, param: parameters, transaction: dbTransaction);
                    dbTransaction.Commit();
                    db.Close();
                    return true;
                }
                catch (System.Exception)
                {
                    dbTransaction.Rollback();
                    db.Close();
                    return false;
                }
            }
        }

    }
}
