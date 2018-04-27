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
    public class GroupController : Controller
    {
        private readonly ISavingsProvider _groupProvider;

        public GroupController()
        {
            _groupProvider = SavingsProviderFactory.Instance.CreateSavingsModelObject("Grupo");
        }

        

        public ActionResult NuevoGrupo()
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

        [HttpPost]
        public ActionResult CrearGrupo(GroupDtoModel groupModel)
        {
            try
            {

                _groupProvider.AddObject(groupModel);
                //creation ok
                TempData["SavingsAction"] = "SOk";
                return RedirectToAction("VerGrupos");
            }
            catch
            {
                //creation failed
                TempData["SavingsAction"] = "SF";
                return RedirectToAction("VerGrupos");
            }
        }


        public ActionResult VerGrupos()
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


        public string GetGrupos()
        {
            var groups = _groupProvider.GetAllObjects() as IEnumerable<GroupDtoModel>;
            return JsonConvert.SerializeObject(groups);
        }


        public string GetGroupById(int idGrupo)
        {
            var groupDtoModel = GetGroupDtoById(idGrupo);
            return JsonConvert.SerializeObject(groupDtoModel);
        }

        public GroupDtoModel GetGroupDtoById(int idGrupo)
        {
            var group = (Grupo)_groupProvider.GetObjectById(idGrupo);

            return new GroupDtoModel
            {
                IdGrupo = group.IdGrupo,
                Nombre = group.Nombre,
                Descripcion = group.Descripcion,
                FechaCreacion = group.Fecha_Creacion
            };
        }

        public ActionResult EditarGrupo(int idGrupo)
        {
            var groupDto = GetGroupDtoById(idGrupo);
            //_groupProvider.UpdateObject(grupo);
            //return RedirectToAction("VerGrupos");
            return View(groupDto);
        }

        [HttpPost]
        public ActionResult EditarGrupo(GroupDtoModel grupo)
        {
            try
            {
                _groupProvider.UpdateObject(grupo);
                //updated ok
                TempData["SavingsAction"] = "UOk";
                return RedirectToAction("VerGrupos");

            }
            catch
            {
                //update failed
                TempData["SavingsAction"] = "UF";
                return RedirectToAction("VerGrupos");
            }
        }

        public void EliminarGrupo(int idGrupo)
        {
            try
            {
                var group = _groupProvider.GetObjectById(idGrupo);
                _groupProvider.DeleteObject(group);
                //delete ok
                TempData["SavingsAction"] = "DOk";
            }
            catch
            {
                //delete failed
                TempData["SavingsAction"] = "DF";
                RedirectToAction("VerGrupos");
            }
        }


    }
}