

using System.Data;
using System.Data.SqlClient;

namespace ADO.Net
{
    /// <summary>
    /// Clase que implementa la interfaz <c>IAdoRepository</c>
    /// </summary>
    public class AdoRepository : IAdoRepository
    {
        private string conecctionString;

        public SqlConnection sqlserver;

        public AdoRepository(String connection)
        {

            conecctionString = connection;
        }        
        
        //metodo para obtener la ciudad por el id
        public Task<CiudadResponseModel> GetCiudadById(string Id)
        {
            //instanciamos un modelo de respuesta
            CiudadResponseModel responseModel = new();
            try
            {
                //usamos temporalmente una conexion a la base de datos
                using (sqlserver = new SqlConnection(conecctionString))
                {
                    //abrimos la conexion
                    sqlserver.Open();

                    //creamos una linea de General de SQL
                    SqlCommand cmd = sqlserver.CreateCommand();

                    cmd.Connection = sqlserver;

                    //quemamos el boceto de la consulta
                    string query = "SELECT ciud_ID, ciud_nombre, pais_ID " +
                                   "FROM Ciudad AS ciud " +
                                   "WHERE ciud.ciud_ID = @ciud_ID;";
                                    
                    //asignamos la consulta a la linea de sql
                    cmd.CommandText = query;

                    //parseamos los parametros
                    cmd.Parameters.AddWithValue("@ciud_ID",Id);

                    //ejecutamos la linea de sql
                    var ciudadFromDB = cmd.ExecuteReader();

                    //comprobamos si hubiern lineas afectadas en la base de datos
                    if(ciudadFromDB.HasRows)
                    {
                        //si las hay, leemos las lineas y las mapeamos al modelo
                        while (ciudadFromDB.Read())
                        {
                            responseModel.ciud_ID = (string)ciudadFromDB["ciud_ID"];
                            responseModel.ciud_nombre = (string)ciudadFromDB["ciud_nombre"];
                            responseModel.pais_ID = (string)ciudadFromDB["pais_ID"];
                        }
                    }
                    else
                    {
                        //si no hay lineas lanzamos una excepcion
                        throw new Exception("The SELECT statement conflicted with Ciud_ID, the conflic occured by sending an invalid ciud_ID, NOT FOUND");
                    }

                    //llenamos el modelo de respuesta acorder a lo acordado
                    responseModel.Succcess = true;
                    responseModel.ErrorDetail = "";
                    responseModel.ErrorNumber = "";
                    responseModel.NumberOfRecords = 1;
                }
            }
            catch (Exception exp)
            {
                //excepcion general, 
                
                responseModel.ciud_ID = null!;
                responseModel.ciud_nombre = null!;
                responseModel.pais_ID = null;
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "-";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }
            finally
            {
                sqlserver.Close();
            }

            return Task.FromResult(responseModel);
        }

        //metodo para obtener todas las ciudades
        public Task<CiudadesResponseModel> GetCiudades()
        {
            CiudadesResponseModel responseModel = new();

            try
            {
                using (sqlserver = new SqlConnection(conecctionString))
                {
                    string query = "SELECT ciud_ID, ciud_nombre, pais_ID " +
                       "FROM Ciudad AS c " +
                       "ORDER BY c.ciud_ID ASC";

                    SqlCommand cmd = new SqlCommand(query, sqlserver);

                    sqlserver.Open();

                    var ciudadesFromDB = cmd.ExecuteReader();

                    if (ciudadesFromDB.HasRows)
                    {
                        List<CiudadModel> listCiudades = new();
                        while (ciudadesFromDB.Read())
                        {
                            listCiudades.Add(new CiudadModel { 
                                ciud_ID = (string)ciudadesFromDB["ciud_ID"],
                                ciud_nombre = (string)ciudadesFromDB["ciud_nombre"],
                                pais_ID = (string)ciudadesFromDB["pais_ID"]
                            });
                        }
                        responseModel.Ciudades = listCiudades;
                    }
                    else
                    {
                        throw new Exception("Not Data Found");
                    }
                    
                    responseModel.Succcess = true;
                    responseModel.ErrorDetail = "";
                    responseModel.ErrorNumber = "";
                    responseModel.NumberOfRecords = responseModel.Ciudades.Count();
                }
            }
            catch (Exception exp)
            {
                responseModel.Ciudades = null!;
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "-";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }
            finally
            {
                sqlserver.Close();
            }

            return Task.FromResult(responseModel);
        }


        //metodo para agregar una ciudad
        public Task<CiudadResponseModel> PostCiudad(CiudadCreateModel ciudad)
        {
            CiudadResponseModel responseModel = new CiudadResponseModel
            {
                ciud_ID = ciudad.ciud_ID,
                ciud_nombre = ciudad.ciud_nombre,
                pais_ID = ciudad.pais_ID
            };
            try
            {
                using (sqlserver = new SqlConnection(conecctionString))
                {
                    sqlserver.Open();

                    SqlCommand cmd = sqlserver.CreateCommand();

                    cmd.Connection = sqlserver;

                    string query = "INSERT INTO CIUDAD(ciud_ID,ciud_nombre,pais_ID)" +
                                  " VALUES(@ciud_ID,@ciud_nombre,@pais_ID);";


                    cmd.Parameters.Clear();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ciud_ID", ciudad.ciud_ID);
                    cmd.Parameters.AddWithValue("@ciud_nombre", ciudad.ciud_nombre);
                    cmd.Parameters.AddWithValue("@pais_ID", ciudad.pais_ID);

                    cmd.ExecuteNonQuery();

                    responseModel.Succcess = true;
                    responseModel.ErrorDetail = "";
                    responseModel.ErrorNumber = "";
                    responseModel.NumberOfRecords = 1;

                }

            }
            //lanzamos un catch con un SqlException para atrapar lso errores de enviados por la base de datos
            catch (SqlException exp)
            {
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "549";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }
            catch (Exception exp)
            {
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "-";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }
            finally
            {
                sqlserver.Close();
            }



            return Task.FromResult(responseModel);
        }


        //metodo  para actualizar una ciudad
        public Task<CiudadResponseModel> UpdateCiudad(string Id, CiudadModifyModel ciudadModify)
        {
           CiudadResponseModel responseModel = new CiudadResponseModel
                                {
                                    ciud_ID = Id,
                                    ciud_nombre = ciudadModify.ciud_nombre,
                                    pais_ID = ciudadModify.pais_ID
                                };
            try
            {
                using (sqlserver = new SqlConnection(conecctionString))
                {
                    sqlserver.Open();

                    SqlCommand cmd = sqlserver.CreateCommand();

                    cmd.Connection = sqlserver;

                    string query =  "UPDATE CIUDAD " +
                                    "SET ciud_nombre = @ciud_nombre, pais_ID = @pais_ID " +
                                    "WHERE ciud_ID = @ciud_ID;";

                    cmd.Parameters.Clear();

                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@ciud_nombre", ciudadModify.ciud_nombre);
                    cmd.Parameters.AddWithValue("@pais_ID", ciudadModify.pais_ID);
                    cmd.Parameters.AddWithValue("@ciud_ID", Id);                  
                    
                    var rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new Exception("The UPDATE statement conflicted with Ciud_ID the conflict occurred by sending an invalid ciud_ID, 0 ROWS AFFECTED, NOT FOUND");
                    }

                    responseModel.Succcess = true;
                    responseModel.ErrorDetail = "";
                    responseModel.ErrorNumber = "";
                    responseModel.NumberOfRecords = 1;

                }

            }
            catch (SqlException exp)
            {
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "547";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }

            catch (Exception exp)
            {
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "-";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }
            finally
            {
                sqlserver.Close();
            }



            return Task.FromResult(responseModel);
        }


        //metodo para eliminar una ciudad
        public Task<CiudadResponseModel> DeleteCiudad(string Id)
        {
            CiudadResponseModel responseModel = new();
            try
            {
                using (sqlserver = new SqlConnection(conecctionString))
                {
                    
                    string query = "DELETE FROM CIUDAD " +
                                   "WHERE ciud_ID =@ciud_ID;";

                    SqlCommand cmd = new SqlCommand(query, sqlserver);

                    cmd.Parameters.AddWithValue("@ciud_ID", Id);

                    sqlserver.Open();


                    var rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new Exception("The DELETE statement conflicted with Ciud_ID the conflict occurred by sending an invalid ciud_ID, 0 ROWS AFFECTED, NOT FOUND");
                    }

                    responseModel.Succcess = true;
                    responseModel.ErrorDetail = "";
                    responseModel.ErrorNumber = "";
                    responseModel.NumberOfRecords = 1;

                }

            }
            catch (SqlException exp)
            {
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "--";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }

            catch (Exception exp)
            {
                responseModel.Succcess = false;
                responseModel.ErrorDetail = exp.Message;
                responseModel.ErrorNumber = "-";
                responseModel.NumberOfRecords = 0;

                return Task.FromResult(responseModel);
            }
            finally
            {
                sqlserver.Close();
            }



            return Task.FromResult(responseModel);
        }
    }
       
}