using System.Data;
using DAL;
using MODELS;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace BLL
{
    public class BL_ALUMNOS
    {
        public static IEnumerable<DtoCatAlumnos> GetAllAlumnos(string PCadena)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = Enumerable.Empty<DtoCatAlumnos>();

            var dpParametros = new
            {
                P_Accion = 1
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spAlumnoCON", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                enuAlumnos = (from item in Dt.AsEnumerable()
                                select new DtoCatAlumnos
                                {
                                    IdAlumno = item.Field<Int32>("IdAlumno"),
                                    Nombre = item.Field<string>("Nombre"),
                                    ApPaterno = item.Field<string>("ApPaterno"),
                                    ApMaterno = item.Field<string>("ApMaterno"),
                                    CURP = item.Field<string>("CURP"),
                                    FechaNacimiento = item.Field<string>("FechaNacimiento"),
                                    Estado = item.Field<string>("Estado")

                                }).AsEnumerable();
            }

            return enuAlumnos;
        }

        public static IEnumerable<DtoCatAlumnos> GetAlumnoID(string PCadena, int IdAlumno)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = Enumerable.Empty<DtoCatAlumnos>();

            var dpParametros = new
            {
                P_Accion = 2,
                P_IdAlumno = IdAlumno,
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spAlumnoCON", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                enuAlumnos = (from item in Dt.AsEnumerable()
                              select new DtoCatAlumnos
                              {
                                  IdAlumno = item.Field<Int32>("IdAlumno"),
                                  Nombre = item.Field<string>("Nombre"),
                                  ApPaterno = item.Field<string>("ApPaterno"),
                                  ApMaterno = item.Field<string>("ApMaterno"),
                                  CURP = item.Field<string>("CURP"),
                                  FechaNacimiento = item.Field<string>("FechaNacimiento"),
                                  Estado = item.Field<string>("Estado")

                              }).AsEnumerable();
            }

            return enuAlumnos;
        }

        public static IEnumerable<DtoCatAlumnos> GetAlumnoCURP(string PCadena, string CURP)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = Enumerable.Empty<DtoCatAlumnos>();

            var dpParametros = new
            {
                P_Accion = 3,
                P_IdAlumno = 0,
                P_CURP = CURP
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spAlumnoCON", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                enuAlumnos = (from item in Dt.AsEnumerable()
                              select new DtoCatAlumnos
                              {
                                  IdAlumno = item.Field<Int32>("IdAlumno"),
                                  Nombre = item.Field<string>("Nombre"),
                                  ApPaterno = item.Field<string>("ApPaterno"),
                                  ApMaterno = item.Field<string>("ApMaterno"),
                                  CURP = item.Field<string>("CURP"),
                                  FechaNacimiento = item.Field<string>("FechaNacimiento"),
                                  Estado = item.Field<string>("Estado")

                              }).AsEnumerable();
            }

            return enuAlumnos;
        }

        public static IEnumerable<DtoCatAlumnos> GetAlumnoTexto(string PCadena, string Texto)
        {
            IEnumerable<DtoCatAlumnos> enuAlumnos = Enumerable.Empty<DtoCatAlumnos>();

            var dpParametros = new
            {
                P_Accion = 5,
                P_IdAlumno = 0,
                P_CURP = "",
                P_Texto = Texto

            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spAlumnoCON", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                enuAlumnos = (from item in Dt.AsEnumerable()
                              select new DtoCatAlumnos
                              {
                                  IdAlumno = item.Field<Int32>("IdAlumno"),
                                  Nombre = item.Field<string>("Nombre"),
                                  ApPaterno = item.Field<string>("ApPaterno"),
                                  ApMaterno = item.Field<string>("ApMaterno"),
                                  CURP = item.Field<string>("CURP"),
                                  FechaNacimiento = item.Field<string>("FechaNacimiento"),
                                  Estado = item.Field<string>("Estado")

                              }).AsEnumerable();
            }

            return enuAlumnos;
        }

        public static IEnumerable<string> ValidaInfoGuardar(string PCadena, DtoAltAlumnos Alumno)
        {
            List<string> lstValidacion = new List<string>();

            DateTime fechaNacimiento;

            Alumno.CURP = Alumno.CURP.Trim();

            if (Alumno.Nombre.Length <= 2)
            {
                lstValidacion.Add("Debe ingresar un nombre");
            }

            if (Alumno.ApPaterno.Length <= 2)
            {
                lstValidacion.Add("Debe ingresar un apellido paterno");
            }

            if (Alumno.ApMaterno.Length <= 2)
            {
                lstValidacion.Add("Debe ingresar un apellido materno");
            }

            if (Alumno.CURP.Length != 18)
            {
                lstValidacion.Add("El CURP debe de contener 18 caracteres");
            }

            if (string.IsNullOrEmpty(Alumno.CURP))
            {
                lstValidacion.Add("El CURP debe de contener 18 caracteres");
            }

            if (Repository.ValidaTexto(Alumno.Nombre))
            {
                lstValidacion.Add("El nombre solo puede contener letras");
            }

            if (Repository.ValidaTexto(Alumno.ApPaterno))
            {
                lstValidacion.Add("El apellido paterno solo puede contener letras");
            }

            if (Repository.ValidaTexto(Alumno.ApMaterno))
            {
                lstValidacion.Add("El apellido materno solo puede contener letras");
            }

            if (!DateTime.TryParse(Alumno.FechNac, out fechaNacimiento))
            {
                lstValidacion.Add("La fecha de nacimiento no es válida");
            }
            else if (fechaNacimiento > DateTime.Now.AddYears(-10))
            {
                lstValidacion.Add("El alumno debe tener por lo menos 10 años");
            }

            if (ValidaCURPDB(PCadena, Alumno.CURP))
            {
                lstValidacion.Add("El CURP ya esta registrado");
            }

            return lstValidacion.AsEnumerable();
        }

        public static IEnumerable<string> ValidaInfoEditar(string PCadena, DtoAltAlumnos Alumno)
        {
            List<string> lstValidacion = new List<string>();

            DateTime fechaNacimiento;

            Alumno.CURP = Alumno.CURP.Trim();

            if (Alumno.Nombre.Length <= 2)
            {
                lstValidacion.Add("Debe ingresar un nombre");
            }

            if (Alumno.ApPaterno.Length <= 2)
            {
                lstValidacion.Add("Debe ingresar un apellido paterno");
            }

            if (Alumno.ApMaterno.Length <= 2)
            {
                lstValidacion.Add("Debe ingresar un apellido materno");
            }

            if (Alumno.CURP.Length != 18)
            {
                lstValidacion.Add("El CURP debe de contener 18 caracteres");
            }

            if (string.IsNullOrEmpty(Alumno.CURP))
            {
                lstValidacion.Add("El CURP debe de contener 18 caracteres");
            }

            if (Repository.ValidaTexto(Alumno.Nombre))
            {
                lstValidacion.Add("El nombre solo puede contener letras");
            }

            if (Repository.ValidaTexto(Alumno.ApPaterno))
            {
                lstValidacion.Add("El apellido paterno solo puede contener letras");
            }

            if (Repository.ValidaTexto(Alumno.ApMaterno))
            {
                lstValidacion.Add("El apellido materno solo puede contener letras");
            }

            if (!DateTime.TryParse(Alumno.FechNac, out fechaNacimiento))
            {
                lstValidacion.Add("La fecha de nacimiento no es válida");
            }
            else if (fechaNacimiento > DateTime.Now.AddYears(-10))
            {
                lstValidacion.Add("El alumno debe tener por lo menos 10 años");
            }

            if (ValidaCURPDB(PCadena, Alumno.IdAlumno,Alumno.CURP))
            {
                lstValidacion.Add("El CURP ya esta registrado");
            }

            return lstValidacion.AsEnumerable();
        }

        private static Boolean ValidaCURPDB(string PCadena, string CURP)
        {
            Boolean Validacion = false;
            var dpParametros = new
            {
                P_Accion = 3,
                P_IdAlumno = 0,
                P_CURP = CURP
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spAlumnoCON", dpParametros);
            if (Dt.Rows.Count > 0)
            {
                Validacion = true;
            }
            return Validacion;
        }

        //Se aplica polimorfismo para este metodo
        private static Boolean ValidaCURPDB(string PCadena, int IdAlumno, string CURP)
        {
            Boolean Validacion = false;
            var dpParametros = new
            {
                P_Accion = 4,
                P_IdAlumno = IdAlumno,
                P_CURP = CURP
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spAlumnoCON", dpParametros);
            if (Dt.Rows.Count > 0)
            {
                Validacion = true;
            }
            return Validacion;
        }

        public static IEnumerable<string> GuardarInfoAlumno(string PCadena, DtoAltAlumnos Alumno)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_Nombre = Alumno.Nombre.Trim(),
                    P_APaterno = Alumno.ApPaterno.Trim(),
                    P_AMaterno = Alumno.ApMaterno.Trim(),
                    P_CURP = Alumno.CURP.Trim(),
                    P_FechNac = Alumno.FechNac 
                };
                Contexto.Procedimiento_StoreDB(PCadena, "spAlumnoALT", dpParametros);
                lstDatos.Add("00");
                lstDatos.Add("El alumno se registro con exito");
            }
            catch (Exception e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }

            return lstDatos.AsEnumerable();

        }

        public static IEnumerable<string> EditarInfoAlumno(string PCadena, DtoAltAlumnos Alumno)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_IdAlumno = Alumno.IdAlumno,
                    P_Nombre = Alumno.Nombre.Trim(),
                    P_APaterno = Alumno.ApPaterno.Trim(),
                    P_AMaterno = Alumno.ApMaterno.Trim(),
                    P_CURP = Alumno.CURP.Trim(),
                    P_FechNac = Alumno.FechNac
                };
                Contexto.Procedimiento_StoreDB(PCadena, "spAlumnoACT", dpParametros);
                lstDatos.Add("00");
                lstDatos.Add("El alumno se modifico con exito");
            }
            catch (Exception e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }

            return lstDatos.AsEnumerable();

        }

        public static List<string> CambiaEstadoAlumno(string Cadena, int IdAlumno)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_IdAlumno = IdAlumno
                };
                Contexto.Procedimiento_StoreDB(Cadena, "spAlumnoEstadoACT", dpParametros);
                lstDatos.Add("00");
                lstDatos.Add("El estado del alumno fue modificado con exito");


            }
            catch (SqlException ex)
            {
                lstDatos.Add("14");
                lstDatos.Add(ex.Message);
            }

            return lstDatos;
        }

        public static List<string> EliminaAlumno(string Cadena, int IdAlumno)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_IdAlumno = IdAlumno
                };
                Contexto.Procedimiento_StoreDB(Cadena, "spAlumnoBAJ", dpParametros);
                lstDatos.Add("00");
                lstDatos.Add("El estado del alumno fue eliminado con exito");


            }
            catch (SqlException ex)
            {
                lstDatos.Add("14");
                lstDatos.Add(ex.Message);
            }

            return lstDatos;
        }


    }
}