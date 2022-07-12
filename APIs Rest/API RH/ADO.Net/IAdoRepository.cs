using Models.CreateModels;
using Models.ResponseModels;
using Models.ModifyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Net
{

    /// <summary>
    /// Interfaz para ADO.NET Repositorio
    /// </summary>
    public interface IAdoRepository
    { 
        /// <summary>
        /// metodo para obtener una ciudad por el id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Modelo del tipo CiudadResponseModel</returns>
        public Task<CiudadResponseModel> GetCiudadById(string Id);

        /// <summary>
        /// Metodo para obtener todas las ciudades
        /// </summary>
        /// <returns>Modelo del tipo CiudadesResponseModel</returns>
        public Task<CiudadesResponseModel> GetCiudades();

          /// <summary>
          /// metodo para agregar una ciudad
          /// </summary>
          /// <param name="ciudad"></param>
          /// <returns></returns>
        public Task<CiudadResponseModel> PostCiudad(CiudadCreateModel ciudad);

        /// <summary>
        /// metodo para actualizar una ciudad
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ciudadModify"></param>
        /// <returns>Modelo del tipo CiudadResponseModel</returns>
        public Task<CiudadResponseModel> UpdateCiudad(string Id, CiudadModifyModel ciudadModify);

        /// <summary>
        /// Modeo para eliminar una ciudad
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Modelo del tipo CiudadResponseModel</returns>
        public Task<CiudadResponseModel> DeleteCiudad(string Id);
    }
}
