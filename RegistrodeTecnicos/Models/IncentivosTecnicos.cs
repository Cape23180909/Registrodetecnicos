using System.ComponentModel.DataAnnotations;

namespace RegistrodeTecnicos.Models
{
    public class IncentivosTecnicos

    {
        [Key]

        public int IncentivoId { get; set; }
        public string Fecha { get; set; }
        public int TecnicoId { get; set; }
        public string Descripcion { get; set; }
        public int CantidadServicios { get; set; }
        public long Monto { get; set; }


    }
    }