using System.ComponentModel.DataAnnotations;

namespace RegistrodeTecnicos.Models;
public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }
    [Required(ErrorMessage = "El campo no esta lleno")]
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "Ingrese un insentivo")]
    public int Incentivo { get; set; }


}