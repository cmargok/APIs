using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModels
{
    /// <summary>
    /// Modelo para la entidad Pais
    /// </summary>
    public class PaisModel

    {
        /// <value>propiedad pais_ID correspondiente al ID del pais</value>      
        public string? pais_ID { get; set; }

        /// <value>propiedad pais_nombre correspondiente al nombre del pais</value>   
        public string? pais_nombre { get; set; }

    }

    /// <summary>
    /// Modelo de respuesta para la entidad Pais, hereda las propiedades de la clase <c>GenereicResponseModel</c>
    /// </summary>
    public class PaisResponseModel : GenericResponseModel
    {
        /// <value>propiedad pais_ID correspondiente al ID del pais</value>      
        public string? pais_ID { get; set; }

        /// <value>propiedad pais_nombre correspondiente al nombre del pais</value>   
        public string? pais_nombre { get; set; }
    }


    /// <summary>
    /// Modelo de respuesta para una lista de entidades Pais, hereda las propiedades de la clase <c>GenereicResponseModel</c>
    /// </summary>
    public class PaisesResponseModel : GenericResponseModel
    {
        /// <value>propiedad Paises del tipo IEnumerable correspondientea lista de paises del tipo Entidad <c>PaisModel</c> </value>  
        public IEnumerable<PaisModel>? Paises { get; set; }
    }
}
