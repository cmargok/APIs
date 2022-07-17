using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.API.DataAccess.Models
{
    [Table("HISTORICO")]
    public partial class HISTORICO
    {
        [Key]
        public int emphist_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime emphist_fecha_retiro { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string emphist_dpto_ID { get; set; } = null!;
        [StringLength(6)]
        [Unicode(false)]
        public string emphist_cargo_ID { get; set; } = null!;
        public int emphist_empl_ID { get; set; }

        [ForeignKey("emphist_cargo_ID")]
        [InverseProperty("HISTORICOs")]
        public virtual CARGO emphist_cargo { get; set; } = null!;
        [ForeignKey("emphist_dpto_ID")]
        [InverseProperty("HISTORICOs")]
        public virtual DEPARTAMENTO emphist_dpto { get; set; } = null!;
        [ForeignKey("emphist_empl_ID")]
        [InverseProperty("HISTORICOs")]
        public virtual EMPLEADO emphist_empl { get; set; } = null!;
    }
}
