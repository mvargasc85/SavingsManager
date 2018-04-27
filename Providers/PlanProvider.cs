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
    public class PlanProvider : ISavingsProvider
    {

        public int IdPlan { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public char Periodicidad { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

        public SavingsDataRepository SavingsDataRepository { get; set; }

        public PlanProvider()
        {
            SavingsDataRepository = new SavingsDataRepository();
        }

        public PlanProvider(SavingsDataRepository savingsDataRepository)
        {
            SavingsDataRepository = savingsDataRepository;
        }


        public IEnumerable<object> GetAllObjects()
        {
            var planes = SavingsDataRepository.GetAllPlanes();
            if (planes == null) return null;

            planes = (IEnumerable<Plan>)planes.ToList();

            var planDtoModelList = new List<PlanDtoModel>();

            planes.ForEach(plan => planDtoModelList.Add(new PlanDtoModel
            {
                IdPlan = plan.IdPlan,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Duracion = plan.Duracion,
                Periodicidad = plan.Periodicidad,
                MontoCuota = plan.MontoCuota,
                FechaInicial = plan.FechaInicial,
                FechaFinal = plan.FechaFinal
            }));

            return planDtoModelList;
        }

        public object GetObjectById(int id)
        {
            return SavingsDataRepository.GetPlanById(id);
        }

        public void AddObject(object item)
        {
            var PlanModel = (PlanDtoModel)item;
            var plan = new Plan
            {
                IdPlan = PlanModel.IdPlan,
                Nombre = PlanModel.Nombre,
                Descripcion = PlanModel.Descripcion,
                Duracion = PlanModel.Duracion,
                Periodicidad = PlanModel.Periodicidad,
                MontoCuota = PlanModel.MontoCuota,
                FechaInicial = PlanModel.FechaInicial,
                FechaFinal = PlanModel.FechaFinal
            };
            SavingsDataRepository.AddPlan(plan);
        }

        public void UpdateObject(object item)
        {
            var planModel = (PlanDtoModel)item;
            SavingsDataRepository.UpdatePlan(planModel);
        }

        public void DeleteObject(object item)
        {
            var plan = (Plan)item;
            SavingsDataRepository.DeletePlan(plan);
        }

        public IEnumerable<SelectListItem> GetPlanSelectList()
        {
            var planes = (IEnumerable<PlanDtoModel>)GetAllObjects();
            var planesSelectList = new List<SelectListItem>();
            foreach (var planesDtoModel in planes)
            {
                planesSelectList.Add(new SelectListItem
                {
                    Text = planesDtoModel.Nombre,
                    Value = planesDtoModel.IdPlan.ToString()
                });
            }

            return planesSelectList;
        }
        
    }
}