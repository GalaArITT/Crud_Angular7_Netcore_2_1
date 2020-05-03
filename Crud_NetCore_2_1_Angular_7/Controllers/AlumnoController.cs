using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_NetCore_2_1_Angular_7.Data.Interfaces;
using Crud_NetCore_2_1_Angular_7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_NetCore_2_1_Angular_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnosRepository al;
        public AlumnoController(IAlumnosRepository al)
        {
            this.al = al;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await al.VerAlumnos());
            }catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost("[action]")]
        public IActionResult Post([FromBody] Alumnos alumnos)
        {
            try
            {
                return Ok(al.InsertarAlumno(alumnos));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("[action]")]
        public IActionResult EliminarAlumno(int id)
        {
            try
            {
                return Ok(al.Eliminar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPut("[action]/{id}")]
        public IActionResult Put(int id, [FromBody] Alumnos alumnos)
        {
            try
            {
                return Ok(al.EditarAlumno(id,alumnos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}