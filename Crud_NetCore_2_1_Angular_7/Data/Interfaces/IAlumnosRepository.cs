using Crud_NetCore_2_1_Angular_7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_NetCore_2_1_Angular_7.Data.Interfaces
{
    public interface IAlumnosRepository
    {
        Task<IEnumerable<Alumnos>> VerAlumnos();
        bool InsertarAlumno(Alumnos alumnos);
        bool Eliminar(int id);
        bool EditarAlumno(int id, Alumnos alumnos);
    }
}
