using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace DAL
{
    public class Contexto
    {

        //SELECT
        public static DataTable Funcion_StoreDB(string PCadena, string PSentencia, object PParametro)
        {
            DataTable Dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(PCadena))
                {
                    var lst = conn.ExecuteReader(PSentencia, PParametro, commandType: CommandType.StoredProcedure);
                    Dt.Load(lst);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return Dt;
        }

        //UPDATE INSERT DELETE
        public static void Procedimiento_StoreDB(string PCadena, string PSentencia, object PParametro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(PCadena))
                {
                    var lst = conn.ExecuteReader(PSentencia, PParametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

        }
    }

}