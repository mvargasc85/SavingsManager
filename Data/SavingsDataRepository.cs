using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SavingsManager.Models.DTOModels;

namespace SavingsManager.Data
{
    public class SavingsDataRepository
    {
        private readonly SavingMgrDbDataContext _savingsDataContext;

        public SavingsDataRepository()
        {
            _savingsDataContext = new SavingMgrDbDataContext();
        }

        #region Group Methods

        public Grupo GetGroupById(int id)
        {
            var group = _savingsDataContext.Grupo.SingleOrDefault(g => g.IdGrupo == id);
            return group;
        }

        public IEnumerable<Grupo> GetAllGroups()
        {
            var groups = _savingsDataContext.Grupo.Select(g => g);
            return groups;
        }

        public void AddGroup(Grupo group)
        {
            _savingsDataContext.Grupo.InsertOnSubmit(group);

            try
            {
                _savingsDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateGroup(GroupDtoModel groupModel)
        {
            var group = (Grupo) GetGroupById(groupModel.IdGrupo);

            group.Nombre = groupModel.Nombre;
            group.Descripcion = groupModel.Descripcion;

            _savingsDataContext.SubmitChanges();

        }


        public void DeleteGroup(Grupo group)
        {
            _savingsDataContext.Grupo.DeleteOnSubmit(group);
            try
            {
                _savingsDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        

       
        #endregion

        #region Socio Methods

        public Socio GetSocioById(int id)
        {
            var socio = _savingsDataContext.Socio.SingleOrDefault(s => s.IdSocio == id);
            return socio;
        }

        public IEnumerable<Socio> GetAllSocios()
        {
            var socios = _savingsDataContext.Socio.Select(g => g);
            return socios;
        }

        public void AddSocio(Socio socio)
        {
            _savingsDataContext.Socio.InsertOnSubmit(socio);

            try
            {
                _savingsDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteSocio(Socio socio)
        {
            _savingsDataContext.Socio.DeleteOnSubmit(socio);
            try
            {
                _savingsDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}