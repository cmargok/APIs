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
    public interface IAdoRepository
    {
      

        public Task<CiudadResponseModel> GetCiudadById(string Id);

        public Task<CiudadesResponseModel> GetCiudades();

        public Task<CiudadResponseModel> PostCiudad(CiudadCreateModel ciudad);

        public Task<CiudadResponseModel> UpdateCiudad(string Id, CiudadModifyModel ciudadModify);

        public Task<CiudadResponseModel> DeleteCiudad(string Id);
    }
}
