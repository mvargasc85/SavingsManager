using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SavingsManager.Data;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;
using SavingsManager.Providers;

namespace SavingsManager.Controllers
{
    public class PlanController : Controller
    {
        private readonly ISavingsProvider _planProvider;

        public PlanController()
        {
            _planProvider = SavingsProviderFactory.CreateSavingsModelObject("Plan");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
        public ActionResult Planes()
        {
            return View();
        }
        public ActionResult NuevoPlan()
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
        public ActionResult VerPlanes()
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
        [HttpPost]
        public ActionResult CrearPlan(PlanDtoModel planModel)
        {
            try
            {

                _planProvider.AddObject(planModel);
                return RedirectToAction("VerPlanes");
            }
            catch 
            {
                return RedirectToAction("VerPlanes");
            }
        }
        [HttpPost]
        public ActionResult EditarPlan(PlanDtoModel plan)
        {
            try
            {
                _planProvider.UpdateObject(plan);
                return RedirectToAction("VerPlanes");

            }
            catch
            {
                return RedirectToAction("VerPlanes");
            }
        }
        public ActionResult EditarPlan(int idPlan)
        {
            var planDto = GetPlanDtoById(idPlan);
            return View(planDto);
        }
        public string GetPlanes()
        {
            var planes = _planProvider.GetAllObjects() as IEnumerable<PlanDtoModel>;
            return JsonConvert.SerializeObject(planes);
        }    
        public void EliminarPlan(int idPlan)
        {
            var plan = _planProvider.GetObjectById(idPlan);
            _planProvider.DeleteObject(plan);
        }
        public PlanDtoModel GetPlanDtoById(int idPlan)
        {
            var plan = (Plan)_planProvider.GetObjectById(idPlan);
            var groupSelectList =  new GroupProvider().GetGroupSelectList();
            return new PlanDtoModel
            {
                IdPlan = plan.IdPlan,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Duracion = plan.Duracion,
                Periodicidad = plan.Periodicidad,
                MontoCuota = plan.MontoCuota,
                FechaInicial = plan.FechaInicial,
                FechaFinal = plan.FechaFinal
            };
        }

       
    }
}