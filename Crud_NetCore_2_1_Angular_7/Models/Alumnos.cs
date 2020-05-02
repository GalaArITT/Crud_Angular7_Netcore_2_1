using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_NetCore_2_1_Angular_7.Models
{
    public class Alumnos
    {
        public int idAlumo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Carrera { get; set; }
    }
}
