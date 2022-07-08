using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModels
{
    public class PaisResponseModel : GenericResponseModel
    {
         public string Pais_ID { get; set; }
        public string Pais_nombre { get; set; }
    }
    public class PaisModel { 
        public string Pais_ID { get; set; }
        public string Pais_nombre { get; set; }
    }

    public class PaisesResponseModel : GenericResponseModel
    {
        public IEnumerable<PaisModel> Paises { get;set ;}
    }
}
