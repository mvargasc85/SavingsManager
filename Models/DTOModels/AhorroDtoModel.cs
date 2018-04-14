using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SavingsManager.Models.DTOModels
{
    public class AhorroDtoModel
    {

        public int idpago { get; set; }
        public int IdPlan { get; set; }
        public int IdSocio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoCuota { get; set; }
        public char Estado { get; set; }
        public IEnumerable<SelectListItem> Planes { get; set; }
        public IEnumerable<SelectListItem> Socios { get; set; }

    }
}