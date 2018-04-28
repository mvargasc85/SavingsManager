using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SavingsManager.Data;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;


namespace SavingsManager.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        private readonly ISavingsProvider _groupReportProvider;
        private readonly SavingMgrDbDataContext _savingsDataContext;

        public ReportController()
        {
            _groupReportProvider = SavingsProviderFactory.Instance.CreateSavingsModelObject("Reporte");
            _savingsDataContext = new SavingMgrDbDataContext();
        }


        public ActionResult VerReporte()
        {
            AccountController account = new AccountController();
            if (Session["SessionIniciada"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public string GetGroupDate(GroupReportDtoModel reportModel)
        {
            var groups = GetObjectsByDate(reportModel.FechaInicial, reportModel.FechaFinal) as IEnumerable<GroupDtoModel>;
            return JsonConvert.SerializeObject(groups);
        }


        public IEnumerable<Grupo> GetGroupByDate(DateTime firstDate, DateTime endDate)
        {
            var groupDate = _savingsDataContext.Grupo.Where(g => g.Fecha_Creacion >= firstDate && g.Fecha_Creacion <= endDate);
            return groupDate;
        }

        public IEnumerable<object> GetObjectsByDate(DateTime firstDate, DateTime endDate)
        {
            var groups = GetGroupByDate(firstDate, endDate).ToList();

            var groupDtoModelList = new List<GroupDtoModel>();

            groups.ForEach(group => groupDtoModelList.Add(new GroupDtoModel
            {
                IdGrupo = group.IdGrupo,
                Nombre = group.Nombre,
                Descripcion = group.Descripcion,
                FechaCreacion = group.Fecha_Creacion
            }));

            return groupDtoModelList;
        }
	}
}