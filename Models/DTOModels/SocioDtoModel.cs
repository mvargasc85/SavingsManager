using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SavingsManager.Models.DTOModels
{
    public class SocioDtoModel
    {

        public int IdSocio { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public int IdGrupo { get; set; }

        public IEnumerable<SelectListItem> Grupos { get; set; }

    }
}