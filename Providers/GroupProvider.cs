using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SavingsManager.Data;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;
using WebGrease.Css.Extensions;

namespace SavingsManager.Providers
{
    /// <summary>
    /// this class is used to data transformation between the datarepository and the controller and viceversa
    /// implememts dependency injection pattern
    /// </summary>
    public class GroupProvider : ISavingsProvider
    {

        public SavingsDataRepository SavingsDataRepository { get; set; }

        public GroupProvider()
        {
            SavingsDataRepository = new SavingsDataRepository();
        }

        /// <summary>
        /// Constructor to implement dependency injection pattern
        /// </summary>
        /// <param name="savingsDataRepository"> receives a reference of savingsDataRepository</param>
        public GroupProvider(SavingsDataRepository savingsDataRepository)
        {
            SavingsDataRepository = savingsDataRepository;
        }

        public IEnumerable<object> GetAllObjects()
        {
            var groups = (IEnumerable<Grupo>) SavingsDataRepository.GetAllGroups().ToList();

            var groupDtoModelList = new List<GroupDtoModel      >();

            groups.ForEach(group => groupDtoModelList.Add(new GroupDtoModel
            {
                IdGrupo = group.IdGrupo,
                Nombre = group.Nombre,
                Descripcion = group.Descripcion,
                FechaCreacion = group.Fecha_Creacion
            }));

            return groupDtoModelList;

        }

        public object GetObjectById(int id)
        {
            return SavingsDataRepository.GetGroupById(id);
        }


        public void AddObject(object item)
        {
            var groupModel = (GroupDtoModel) item;
            var group = new Grupo
            {
                Nombre = groupModel.Nombre,
                Descripcion = groupModel.Descripcion,
                Fecha_Creacion = DateTime.Now
            };
            SavingsDataRepository.AddGroup(group);
        }

        public void UpdateObject(object item)
        {
            var groupModel = (GroupDtoModel) item;
            SavingsDataRepository.UpdateGroup(groupModel);
        }

        public void DeleteObject(object item)
        {
            var grupo = (Grupo) item;
            SavingsDataRepository.DeleteGroup(grupo);
        }

        public IEnumerable<SelectListItem> GetGroupSelectList()
        {
            var grupos = (IEnumerable<GroupDtoModel>) GetAllObjects();
            var groupSelectList = new List<SelectListItem>();
            foreach (var groupDtoModel in grupos)
            {
                groupSelectList.Add(new SelectListItem
                {
                    Text = groupDtoModel.Nombre,
                    Value = groupDtoModel.IdGrupo.ToString()
                });
            }

            return groupSelectList;
        }
        
    }
}