

using System.ComponentModel.DataAnnotations;

namespace Models.CreateModels
{
    public class PaisCreateModel
    {
        [StringLength(3)]
        public string Pais_Id { get; set; }
        public string Pais_nombre { get; set; }

    }
}
