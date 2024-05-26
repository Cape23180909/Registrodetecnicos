using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrodeTecnicos.Models
{
    public class IncentivosTecnicos
    {
        [Key]
        public int IncentivoId { get; set; }
        [Required(ErrorMessage = "El campo fecha no esta lleno")]
        public DateTime Fecha { get; set; }
        public int TecnicoId { get; set; }
        [Required(ErrorMessage = "El campo descripcion no esta lleno")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo cantidad de servicios no esta lleno")]
        public int? CantidadServicios { get; set; }
        [Required(ErrorMessage = "El campo Monto no esta lleno")]

        [ForeignKey("Tecnicos")]
        public decimal? Monto { get; set; }
    }
}