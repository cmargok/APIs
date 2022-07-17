using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.API.DataAccess.Models
{
    [Table("CARGO")]
    public partial class CARGO
    {
        public CARGO()
        {
            EMPLEADOs = new HashSet<EMPLEADO>();
            HISTORICOs = new HashSet<HISTORICO>();
        }

        [Key]
        [StringLength(6)]
        [Unicode(false)]
        public string cargo_ID { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string cargo_nombre { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal cargo_sueldo_minimo { get; set; }
        [Column(TypeName = "money")]
        public decimal cargo_sueldo_maximo { get; set; }

        [InverseProperty("empl_cargo")]
        public virtual ICollection<EMPLEADO> EMPLEADOs { get; set; }
        [InverseProperty("emphist_cargo")]
        public virtual ICollection<HISTORICO> HISTORICOs { get; set; }
    }
}
