
using DataContext.Models;
using Models.CreateModels;
using Models.ModifyModel;
using Models.ResponseModels;
using System.Data;
using System.Data.SqlClient;

namespace ADO.Net
{
    public class AdoRepository : IAdoRepository
    {
        private string conecctionString;

        public SqlConnection sqlserver;

        public AdoRepository(String connection)
        {

            conecctionString = connection;
        }        

        public Task<CiudadResponseModel> GetCiudadById(string Id)
        {
            CiudadResponseModel responseModel = new();
            try
            {
                using (sqlserver = new SqlConnection(conecctionString))
                {
                    sqlserver.Open();

                    SqlCommand cmd = sqlserver.CreateCommand();

                    cmd.Connection = sqlserver;

                    string query = "SELECT ciud_ID, ciud_nombre, pais_ID " +
                                   "FROM Ciudad AS ciud " +
                                   "WHERE ciud.ciud_ID = @ciud_ID;";
                                    
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@ciud_ID",Id);

                    var ciudadFromDB = cmd.ExecuteReader();

                    if(ciudadFromDB.HasRows)
                    {
                        while (ciudadFromDB.Read())
                        {
                            responseModel.ciud_ID = (string)ciudadFromDB["ciud_ID"];
                            responseModel.ciud_nombre = (string)ciudadFromDB["ciud_nombre"];
                            responseModel.pais_ID = (string)ciudadFromDB["pais_ID"];
                        }
                    }
                    else
                    {
                        throw new Exception("The SELECT statement conflicted with Ciud_ID, the conflic occured by sending an invalid ciud_ID, NOT FOUND");
                    }

                    responseModel.Succcess = true;
                    responseModel.ErrorDetail = "";
                    responseModel.ErrorNumber = "";
                    responseModel.NumberOfRecords = 1;
                }
            }
            catch (Exception exp)
            {
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