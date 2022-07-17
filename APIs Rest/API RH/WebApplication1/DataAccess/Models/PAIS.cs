using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.API.DataAccess.Models
{
    [Table("PAIS")]
    public partial class PAIS
    {
        public PAIS()
        {
            CIUDADs = new HashSet<CIUDAD>();
        }

        [Key]
        [StringLength(3)]
        [Unicode(false)]
        public string pais_ID { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string pais_nombre { get; set; } = null!;

        [InverseProperty("pais")]
        public virtual ICollection<CIUDAD> CIUDADs { get; set; }
    }
}
