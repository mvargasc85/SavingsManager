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
    public class AhorroController : Controller
    {
        private readonly ISavingsProvider _ahorroProvider;

        public AhorroController()
        {

                _ahorroProvider = SavingsProviderFactory.CreateSavingsModelObject("Ahorro");

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
      
        public ActionResult Ahorros()
        {
            return View();
        }
        public ActionResult NuevoAhorro()
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
        public ActionResult VerAhorros()
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
        public ActionResult CrearAhorro(AhorroDtoModel ahorroModel)
        {
            try
            {

                _ahorroProvider.AddObject(ahorroModel);
                return RedirectToAction("VerAhorros");
            }
            catch 
            {
                return RedirectToAction("VerAhorros");
            }
        }
        [HttpPost]
        public ActionResult EditarAhorro(AhorroDtoModel ahorro)
        {
            try
            {
                _ahorroProvider.UpdateObject(ahorro);
                return RedirectToAction("VerAhorros");

            }
            catch
            {
                return RedirectToAction("VerAhorros");
            }
        }
        public ActionResult EditarAhorro(int idpago)
        {
            var ahorroDto = GetAhorroDtoById(idpago);
            return View(ahorroDto);
        }
        public string GetAhorros()
        {
            var ahorros = _ahorroProvider.GetAllObjects() as IEnumerable<AhorroDtoModel>;
            return JsonConvert.SerializeObject(ahorros);
        }    
        public void EliminarAhorro(int idpago)
        {
            var ahorro = _ahorroProvider.GetObjectById(idpago);
            _ahorroProvider.DeleteObject(ahorro);
        }
        public AhorroDtoModel GetAhorroDtoById(int idpago)
        {
            var ahorro = (Ahorro)_ahorroProvider.GetObjectById(idpago);
            var planSelectList =  new PlanProvider().GetPlanSelectList();
            var socioSelectList = new SocioProvider().GetSocioSelectList();
            return new AhorroDtoModel
            {
                idpago = ahorro.idpago,
                IdPlan = ahorro.IdPlan,
                IdSocio = ahorro.IdSocio,
                Fecha = ahorro.Fecha,
                MontoCuota = ahorro.MontoCuota,
                Estado = ahorro.Estado,
                Planes = planSelectList,
                Socios = socioSelectList
            };
        }

       
    }
}