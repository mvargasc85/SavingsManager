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
    public class HomeController : Controller
    {
        private readonly ISavingsProvider _groupProvider;
        private readonly ISavingsProvider _planProvider;

        public HomeController()
        {
            _groupProvider = SavingsProviderFactory.CreateSavingsModelObject("Grupo");
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

        public ActionResult VerGrupos()
        {
            return View();
        }


        public string GetGrupos()
        {
            var groups = _groupProvider.GetAllObjects() as IEnumerable<GroupDtoModel>;
            return JsonConvert.SerializeObject(groups);
        }

        public string GetGroupById(int idGrupo)
        {
            var group = (Grupo)_groupProvider.GetObjectById(idGrupo);

            var groupDtoModel = new GroupDtoModel
            {
                IdGrupo = group.IdGrupo,
                Nombre = group.Nombre,
                Descripcion = group.Descripcion,
                FechaCreacion = group.Fecha_Creacion
            };
            return JsonConvert.SerializeObject(groupDtoModel);
        }

        public ActionResult EditarGrupo(GroupDtoModel grupo)
        {
            _groupProvider.UpdateObject(grupo);
            return RedirectToAction("VerGrupos");
        }

        public void EliminarGrupo(int idGrupo)
        {
            var group = _groupProvider.GetObjectById(idGrupo);
            _groupProvider.DeleteObject(group);
        }
        
    }
}