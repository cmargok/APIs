using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.API.DataAccess.Models
{
    [Table("LOCALIZACION")]
    public partial class LOCALIZACION
    {
        [Key]
        [StringLength(5)]
        [Unicode(false)]
        public string localiz_ID { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string localiz_nombre { get; set; } = null!;
        [StringLength(3)]
        [Unicode(false)]
        public string? ciud_ID { get; set; }

        [ForeignKey("ciud_ID")]
        [InverseProperty("LOCALIZACIONs")]
        public virtual CIUDAD? ciud { get; set; }
    }
}
