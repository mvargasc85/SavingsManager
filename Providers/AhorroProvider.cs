using System;
using System.Collections.Generic;
using System.Linq;
using SavingsManager.Data;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;
using WebGrease.Css.Extensions;

namespace SavingsManager.Providers
{
    public class AhorroProvider : ISavingsProvider
    {

        public int idpago { get; set; }
        public int IdPlan { get; set; }
        public int IdSocio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoCuota { get; set; }
        public char Estado { get; set; }

        public SavingsDataRepository SavingsDataRepository { get; set; }

        public AhorroProvider()
        {
            SavingsDataRepository = new SavingsDataRepository();
        }

        public AhorroProvider(SavingsDataRepository savingsDataRepository)
        {
            SavingsDataRepository = savingsDataRepository;
        }


        public IEnumerable<object> GetAllObjects()
        {
            var ahorros = (IEnumerable<Ahorro>)SavingsDataRepository.GetAllAhorros().ToList();

            var ahorroDtoModelList = new List<AhorroDtoModel>();

            ahorros.ForEach(ahorro => ahorroDtoModelList.Add(new AhorroDtoModel
            {
                idpago = ahorro.idpago,
                IdPlan = ahorro.IdPlan,
                IdSocio = ahorro.IdSocio,
                Fecha = ahorro.Fecha,
                MontoCuota = ahorro.MontoCuota,
                Estado = ahorro.Estado
            }));

            return ahorroDtoModelList;
        }

        public object GetObjectById(int id)
        {
            return SavingsDataRepository.GetAhorroById(id);
        }

        public void AddObject(object item)
        {
            var ahorroModel = (AhorroDtoModel)item;
            var ahorro = new Ahorro
            {
                idpago = ahorroModel.idpago,
                IdPlan = ahorroModel.IdPlan,
                IdSocio = ahorroModel.IdSocio,
                Fecha = ahorroModel.Fecha,
                MontoCuota = ahorroModel.MontoCuota,
                Estado = ahorroModel.Estado,
            };
            SavingsDataRepository.AddAhorro(ahorro);
        }

        public void UpdateObject(object item)
        {
            var ahorroModel = (AhorroDtoModel)item;
            SavingsDataRepository.UpdateAhorro(ahorroModel);
        }

        public void DeleteObject(object item)
        {
            var ahorro = (Ahorro)item;
            SavingsDataRepository.DeleteAhorro(ahorro);
        }
    }
}