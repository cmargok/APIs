using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.API.Models
{
    public class GenericResponseModel
    {
        /// <value>propiedad Succes, correspondiente a si el request es o no exitoso</value>  
        public bool Succcess { get; set; }

        /// <value>propiedad ErrorNumber, correspondiente al numero de error generado</value> 
        public string? ErrorNumber { get; set; }

        /// <value>propiedad ErrorDetail, correspondiente al mensaje de error</value> 
        public string? ErrorDetail { get; set; }

        /// <value>propiedad NumberOfRecord, correspondiente a la cantidad de datos para enviar</value> 
        public int NumberOfRecords { get; set; }
    }
}
