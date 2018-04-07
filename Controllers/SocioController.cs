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
    public class SocioController : Controller
    {
        private readonly ISavingsProvider _socioProvider;

        public SocioController()
        {
            _socioProvider = SavingsProviderFactory.CreateSavingsModelObject("Socio");
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
        [HttpPost]
        public ActionResult EditarSocio(SocioDtoModel socio)
        {
            try
            {
                _socioProvider.UpdateObject(socio);
                return RedirectToAction("VerSocios");

            }
            catch
            {
                return RedirectToAction("VerSocios");
            }
        }
        public ActionResult EditarSocio(int idSocio)
        {
            var socioDto = GetSocioDtoById(idSocio);
            return View(socioDto);
        }
        public string GetSocios()
        {
            var socios = _socioProvider.GetAllObjects() as IEnumerable<SocioDtoModel>;
            return JsonConvert.SerializeObject(socios);
        }    
        public void EliminarSocio(int idSocio)
        {
            var socio = _socioProvider.GetObjectById(idSocio);
            _socioProvider.DeleteObject(socio);
        }
        public SocioDtoModel GetSocioDtoById(int idSocio)
        {
            var socio = (Socio)_socioProvider.GetObjectById(idSocio);
            var groupSelectList =  new GroupProvider().GetGroupSelectList();
            return new SocioDtoModel
            {
                IdSocio = socio.IdSocio,
                Nombre = socio.Nombre,
                Apellido1 = socio.Apellido1,
                Apellido2 = socio.Apellido2,
                Email = socio.Email,
                IdGrupo = socio.IdGrupo,
                Grupos = groupSelectList
            };
        }

       
    }
}