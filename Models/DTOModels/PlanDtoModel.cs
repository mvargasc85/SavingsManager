using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SavingsManager.Models.DTOModels
{
    public class PlanDtoModel
    {

        public int IdPlan { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public char Periodicidad { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }


    }
}