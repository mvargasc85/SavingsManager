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

                _ahorroProvider = SavingsProviderFactory.Instance.CreateSavingsModelObject("Ahorro");

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
                 return RedirectToAction("Login", "Account");
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
                 return RedirectToAction("Login", "Account");
            }
            else
            {
                if (TempData["SavingsAction"] != null)
                    ViewBag.SavingsAction = TempData["SavingsAction"].ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult CrearAhorro(AhorroDtoModel ahorroModel)
        {
            try
            {

                _ahorroProvider.AddObject(ahorroModel);
                //creation ok
                TempData["SavingsAction"] = "SOk";
                return RedirectToAction("VerAhorros");
            }
            catch 
            {
                //creation failed
                TempData["SavingsAction"] = "SF";
                return RedirectToAction("VerAhorros");
            }
        }
        [HttpPost]
        public ActionResult EditarAhorro(AhorroDtoModel ahorro)
        {
            try
            {
                _ahorroProvider.UpdateObject(ahorro);
                //update ok
                TempData["SavingsAction"] = "UOk";
                return RedirectToAction("VerAhorros");

            }
            catch
            {
                //update failed
                TempData["SavingsAction"] = "SOk";
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
            try
            {
                var ahorro = _ahorroProvider.GetObjectById(idpago);
                _ahorroProvider.DeleteObject(ahorro);
                //delete ok
                TempData["SavingsAction"] = "DOk";
            }
            catch
            {
                //delete failed
                TempData["SavingsAction"] = "DF";
                RedirectToAction("VerAhorros");
            }
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