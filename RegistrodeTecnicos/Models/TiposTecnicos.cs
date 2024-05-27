using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrodeTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }
    [Required(ErrorMessage = "El campo descripcion no esta lleno")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "El campo incentivo no esta lleno")]
    public decimal? Incentivo { get; set; }
}