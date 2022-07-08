using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models
{
    [Keyless]
    [Table("LOCALIZACION_DEPARTAMENTO")]
    public partial class LOCALIZACION_DEPARTAMENTO
    {
        [StringLength(5)]
        [Unicode(false)]
        public string localiz_ID { get; set; } = null!;
        [StringLength(4)]
        [Unicode(false)]
        public string? dpto_ID { get; set; }

        [ForeignKey("dpto_ID")]
        public virtual DEPARTAMENTO? dpto { get; set; }
        [ForeignKey("localiz_ID")]
        public virtual LOCALIZACION localiz { get; set; } = null!;
    }
}
