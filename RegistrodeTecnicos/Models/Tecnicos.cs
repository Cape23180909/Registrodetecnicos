using System.ComponentModel.DataAnnotations;

namespace RegistrodeTecnicos.Models
{
    public class Tecnicos
    {
        [Key]

        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "El campo no esta lleno")] 
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo no esta lleno")]
        public float? Sueldohora { get; set; }
       


    }
}

