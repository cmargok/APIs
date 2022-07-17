using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.API.DataAccess.Models
{
    [Table("EMPLEADO")]
    public partial class EMPLEADO
    {
        public EMPLEADO()
        {
            HISTORICOs = new HashSet<HISTORICO>();
            Inverseempl_Gerente = new HashSet<EMPLEADO>();
        }

        [Key]
        public int empl_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string empl_primer_nombre { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? empl_segundo_nombre { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? empl_email { get; set; }
        [Column(TypeName = "money")]
        public decimal empl_sueldo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? empl_estado { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public decimal? empl_comision { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string empl_cargo_ID { get; set; } = null!;
        public int empl_Gerente_ID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string empl_dpto_ID { get; set; } = null!;

        [ForeignKey("empl_Gerente_ID")]
        [InverseProperty("Inverseempl_Gerente")]
        public virtual EMPLEADO empl_Gerente { get; set; } = null!;
        [ForeignKey("empl_cargo_ID")]
        [InverseProperty("EMPLEADOs")]
        public virtual CARGO empl_cargo { get; set; } = null!;
        [ForeignKey("empl_dpto_ID")]
        [InverseProperty("EMPLEADOs")]
        public virtual DEPARTAMENTO empl_dpto { get; set; } = null!;
        [InverseProperty("emphist_empl")]
        public virtual ICollection<HISTORICO> HISTORICOs { get; set; }
        [InverseProperty("empl_Gerente")]
        public virtual ICollection<EMPLEADO> Inverseempl_Gerente { get; set; }
    }
}
