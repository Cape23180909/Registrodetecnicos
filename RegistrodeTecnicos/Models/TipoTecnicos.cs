using System.ComponentModel.DataAnnotations;

namespace RegistrodeTecnicos.Models
{
    public class TipoTecnicos
    {
        [Key]

        public int TipoId { get; set; }
        public string Descripcion { get; set; }
      

    }
}
