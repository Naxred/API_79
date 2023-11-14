using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS;

namespace API_79.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlumnosController : ControllerBase
    {

        private readonly string Cadena;

        public AlumnosController(IConfiguration config)
        {
            Cadena = config.GetConnectionString("PROD");
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = BLL.BL_ALUMNOS.GetAllAlumnos(Cadena);
            return Ok(new { Respuesta = enuAlumnos });
        }

        [HttpPost]
        [Route("GetAlumnoID")]

        public IActionResult GetAlumnoID([FromBody] DtoBusquedaAlumno Alumno)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = BLL.BL_ALUMNOS.GetAlumnoID(Cadena, Alumno.IdAlumno);
            return Ok(new { Respuesta = enuAlumnos });
        }

        [HttpPost]
        [Route("GetAlumnoCURP")]

        public IActionResult GetAlumnoCURP([FromBody] DtoBusquedaAlumno Alumno)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = BLL.BL_ALUMNOS.GetAlumnoCURP(Cadena, Alumno.CURP);
            return Ok(new { Respuesta = enuAlumnos });
        }

        [HttpPost]
        [Route("GetAlumnoTexto")]

        public IActionResult GetAlumnoTexto([FromBody] DtoBusquedaAlumno Alumno)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = BLL.BL_ALUMNOS.GetAlumnoTexto(Cadena, Alumno.Texto);
            return Ok(new { Respuesta = enuAlumnos });
        }


        [HttpPost]
        [Route("GuardarAlumno")]
        public IActionResult Guardar([FromBody] DtoAltAlumnos Alumno)
        {
            IEnumerable<string> enuValidaciones = BLL.BL_ALUMNOS.ValidaInfoGuardar(Cadena, Alumno);
            if (!enuValidaciones.Any())
            {

                IEnumerable<string> enuDatos = BLL.BL_ALUMNOS.GuardarInfoAlumno(Cadena, Alumno);



                if (enuDatos.ToList()[0]=="00") 
                {
                    var twilioService = new BL_TwilioSmsService("AC693bf4696ec5c8f4d1f71f40a827cb26", "bf26ae0ee8bee0d22c815289884f4b20", "+15597427032");
                    twilioService.SendSms("+528117044637", "Alumno nuevo registrado");
                    return Ok(new {Code = enuDatos.ToList()[0], Respuesta = enuDatos.ToList()[1] });    
                }
                else 
                {
                    return Ok(new { Code = enuDatos.ToList()[0], Respuesta = enuDatos.ToList()[1] });
                }
            }
            {
                //En caso de falla en reglas de negocio
                return Ok(new { Code = 14, Respuesta = enuValidaciones });
            }


        }


        [HttpPut]
        [Route("EditarInfoAlumno")]
        public IActionResult EditarInfoAlumno([FromBody] DtoAltAlumnos Alumno)
        {
            IEnumerable<string> enuValidaciones = BLL.BL_ALUMNOS.ValidaInfoEditar(Cadena, Alumno);
            if (!enuValidaciones.Any())
            {

                IEnumerable<string> enuDatos = BLL.BL_ALUMNOS.EditarInfoAlumno(Cadena, Alumno);
                if (enuDatos.ToList()[0] == "00")
                {
                    return Ok(new { Code = enuDatos.ToList()[0], Respuesta = enuDatos.ToList()[1] });
                }
                else
                {
                    return Ok(new { Code = enuDatos.ToList()[0], Respuesta = enuDatos.ToList()[1] });
                }
            }
            {
                //En caso de falla en reglas de negocio
                return Ok(new { Code = 14, Respuesta = enuValidaciones });
            }


        }


        [HttpPut]
        [Route("CambiaEstadoAlumno")]
        public IActionResult CambiaEstadoAlumno([FromBody] DtoBusquedaAlumno Alumno)
        {

            List<string> lstDatos = BL_ALUMNOS.CambiaEstadoAlumno(Cadena, Alumno.IdAlumno);

            if (lstDatos[0] == "00")
            {
                return Ok(new { codigo = "00", response = "OK" });
            }
            else
            {
                return Ok(new { codigo = lstDatos[0], response = lstDatos[1] });
            }

        }

        [HttpDelete]
        [Route("EliminaAlumno")]
        public IActionResult EliminaAlumno([FromBody] DtoBusquedaAlumno Alumno)
        {

            List<string> lstDatos = BL_ALUMNOS.EliminaAlumno(Cadena, Alumno.IdAlumno);

            if (lstDatos[0] == "00")
            {
                return Ok(new { codigo = "00", response = "OK" });
            }
            else
            {
                return Ok(new { codigo = lstDatos[0], response = lstDatos[1] });
            }

        }



    }
}
