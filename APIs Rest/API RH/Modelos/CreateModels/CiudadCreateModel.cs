using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CreateModels
{
    public class CiudadCreateModel
    {
        [StringLength(3)]
        public string ciud_ID { get; set; } = null!;
        [StringLength(50)]
        public string ciud_nombre { get; set; } = null!;

        [StringLength(3)]
        public string? pais_ID { get; set; }

    }
}
