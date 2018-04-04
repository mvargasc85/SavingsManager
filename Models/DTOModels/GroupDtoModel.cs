using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SavingsManager.Models.DTOModels
{
    public class GroupDtoModel
    {

        public int IdGrupo { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}