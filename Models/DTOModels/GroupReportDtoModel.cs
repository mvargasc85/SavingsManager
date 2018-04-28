using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SavingsManager.Models.DTOModels
{
    public class GroupReportDtoModel
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}