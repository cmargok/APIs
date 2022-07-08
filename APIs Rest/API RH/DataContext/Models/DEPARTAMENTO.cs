using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models
{
    [Table("DEPARTAMENTO")]
    public partial class DEPARTAMENTO
    {
        public DEPARTAMENTO()
        {
            EMPLEADOs = new HashSet<EMPLEADO>();
            HISTORICOs = new HashSet<HISTORICO>();
        }

        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string dpto_ID { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string dpto_nombre { get; set; } = null!;

        [InverseProperty("empl_dpto")]
        public virtual ICollection<EMPLEADO> EMPLEADOs { get; set; }
        [InverseProperty("emphist_dpto")]
        public virtual ICollection<HISTORICO> HISTORICOs { get; set; }
    }
}
