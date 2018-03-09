using System;
using System.Collections.Generic;
using SavingsManager.Data;
using SavingsManager.Factory;

namespace SavingsManager.Providers
{
    public class PlanProvider : ISavingsProvider
    {

        public int IdPlan { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
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


        public IEnumerable<object> GetAllObjects()
        {
            throw new NotImplementedException();
        }

        public object GetObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddObject(object item)
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(object item)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(object item)
        {
            throw new NotImplementedException();
        }
    }
}