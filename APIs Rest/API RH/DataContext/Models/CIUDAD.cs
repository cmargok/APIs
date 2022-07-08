using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models
{
    [Table("CIUDAD")]
    public partial class CIUDAD
    {
        public CIUDAD()
        {
            LOCALIZACIONs = new HashSet<LOCALIZACION>();
        }

        [Key]
        [StringLength(3)]
        [Unicode(false)]
        public string ciud_ID { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string ciud_nombre { get; set; } = null!;
        [StringLength(3)]
        [Unicode(false)]
        public string? pais_ID { get; set; }

        [ForeignKey("pais_ID")]
        [InverseProperty("CIUDADs")]
        public virtual PAIS? pais { get; set; }
        [InverseProperty("ciud")]
        public virtual ICollection<LOCALIZACION> LOCALIZACIONs { get; set; }
    }
}
