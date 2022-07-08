using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModifyModel
{
    public class CiudadModifyModel
    {        
           
            [StringLength(50)]
            public string ciud_nombre { get; set; } = null!;

            [StringLength(3)]
            public string? pais_ID { get; set; }

       
    }
}
