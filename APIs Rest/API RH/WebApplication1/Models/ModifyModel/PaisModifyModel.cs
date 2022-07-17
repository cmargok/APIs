using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.API.Models.ModifyModel
{
    public class PaisModifyModel
    {
        [StringLength(3)]
        public string Pais_Id { get; set; }
        public string Pais_nombre { get; set; }
    }

    public class PaisModifyNameModel
    {
       public string Pais_nombre { get; set; }
    }
}
