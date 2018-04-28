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
          
            var group =  _savingsDataContext.Grupo.SingleOrDefault(g => g.IdGrupo == id);
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

        public void UpdateSocio(SocioDtoModel socioModel)
        {
            var socio = (Socio)GetSocioById(socioModel.IdSocio);

            socio.Nombre = socioModel.Nombre;
            socio.Apellido1 = socioModel.Apellido1;
            socio.Apellido2 = socioModel.Apellido2;
            socio.Email = socioModel.Email;
            socio.IdGrupo = socioModel.IdGrupo;

            _savingsDataContext.SubmitChanges();

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

        #region Plan Methods

        public Plan GetPlanById(int id)
        {
            var plan = _savingsDataContext.Plan.SingleOrDefault(s => s.IdPlan == id);
            return plan;
        }

        public IEnumerable<Plan> GetAllPlanes()
        {
            var planes = _savingsDataContext.Plan.Select(g => g);
            return planes;
        }

        public void AddPlan(Plan plan)
        {
            _savingsDataContext.Plan.InsertOnSubmit(plan);

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

        public void UpdatePlan(PlanDtoModel planModel)
        {
            var plan = (Plan)GetPlanById(planModel.IdPlan);

            plan.Nombre = planModel.Nombre;
            plan.Descripcion = planModel.Descripcion;
            plan.Duracion = planModel.Duracion;
            plan.MontoCuota = planModel.MontoCuota;
            plan.Periodicidad = planModel.Periodicidad;
            plan.FechaInicial = planModel.FechaInicial;
            plan.FechaFinal = planModel.FechaFinal;
        
            _savingsDataContext.SubmitChanges();

        }
        public void DeletePlan(Plan plan)
        {
            _savingsDataContext.Plan.DeleteOnSubmit(plan);
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

        #region Ahorro Methods

        public Ahorro GetAhorroById(int id)
        {
            var ahorro = _savingsDataContext.Ahorro.SingleOrDefault(a => a.idpago == id);
            return ahorro;
        }

        public IEnumerable<Ahorro> GetAllAhorros()
        {
            var ahorros = _savingsDataContext.Ahorro.Select(a => a);
            return ahorros;
        }

        public void AddAhorro(Ahorro ahorro)
        {
            _savingsDataContext.Ahorro.InsertOnSubmit(ahorro);

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

        public void UpdateAhorro(AhorroDtoModel ahorroModel)
        {
            var ahorro = (Ahorro)GetAhorroById(ahorroModel.idpago);

            ahorro.IdPlan = ahorroModel.IdPlan;
            ahorro.IdSocio = ahorroModel.IdSocio;
            ahorro.Fecha = ahorroModel.Fecha;
            ahorro.MontoCuota = ahorroModel.MontoCuota;
            ahorro.Estado = ahorroModel.Estado;

            _savingsDataContext.SubmitChanges();

        }
        public void DeleteAhorro(Ahorro ahorro)
        {
            _savingsDataContext.Ahorro.DeleteOnSubmit(ahorro);
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