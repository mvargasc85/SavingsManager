using System;
using System.Collections.Generic;
using System.Linq;
using SavingsManager.Data;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;
using WebGrease.Css.Extensions;

namespace SavingsManager.Providers
{
    public class GroupProvider : ISavingsProvider
    {

        public SavingsDataRepository SavingsDataRepository { get; set; }

        public GroupProvider()
        {
            SavingsDataRepository = new SavingsDataRepository();
        }

        public IEnumerable<object> GetAllObjects()
        {
            var groups = (IEnumerable<Grupo>) SavingsDataRepository.GetAllGroups().ToList();

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
            throw new NotImplementedException();
        }

        public void DeleteObject(object item)
        {
            var grupo = (Grupo) item;
            SavingsDataRepository.DeleteGroup(grupo);
        }
    }
}