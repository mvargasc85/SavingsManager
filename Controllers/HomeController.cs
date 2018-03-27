using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;

namespace SavingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISavingsProvider _groupProvider;
        private readonly ISavingsProvider _socioProvider;
        private readonly ISavingsProvider _planProvider;

        public HomeController()
        {
            _groupProvider = SavingsProviderFactory.CreateSavingsModelObject("Grupo");
            _socioProvider = SavingsProviderFactory.CreateSavingsModelObject("Socio");
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

        public ActionResult Grupos()
        {
            return View();
        }

        public ActionResult NuevoGrupo()
        {
            return View();
        }

        public ActionResult Socios()
        {
            return View();
        }

        public ActionResult NuevoSocio()
        {
            return View();
        }

        public ActionResult VerSocios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearGrupo(GroupDtoModel groupModel)
        {
            try
            {
               
                _groupProvider.AddObject(groupModel);
                return RedirectToAction("VerGrupos");
            }
            catch
            {
                return RedirectToAction("VerGrupos");
            }
        }

        [HttpPost]
        public ActionResult CrearSocio(SocioDtoModel socioModel)
        {
            try
            {

                _socioProvider.AddObject(socioModel);
                return RedirectToAction("VerSocios");
            }
            catch 
            {
                return RedirectToAction("VerSocios");
            }
        }

        public ActionResult VerGrupos()
        {
            return View();
        }


        public string GetGrupos()
        {
            var groups = _groupProvider.GetAllObjects() as IEnumerable<GroupDtoModel>;
            return JsonConvert.SerializeObject(groups);
        }


        public string GetSocios()
        {
            var socios = _socioProvider.GetAllObjects() as IEnumerable<SocioDtoModel>;
            return JsonConvert.SerializeObject(socios);
        }
        public void EliminarGrupo(int idGrupo)
        {
            var group = _groupProvider.GetObjectById(idGrupo);
            _groupProvider.DeleteObject(group);
        }

        public void EliminarSocio(int idSocio)
        {
            var socio = _socioProvider.GetObjectById(idSocio);
            _socioProvider.DeleteObject(socio);
        }

    }
}