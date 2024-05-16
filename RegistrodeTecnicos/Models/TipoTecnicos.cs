using System.ComponentModel.DataAnnotations;

namespace RegistrodeTecnicos.Models
{
    public class TipoTecnicos
    {
        [Key]

        public int TecnicoId { get; set; }
        [Required(ErrorMessage = "El campo no esta lleno del tipo tecnico")]
        public string Descripcion { get; set; }
    }
}
