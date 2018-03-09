using System.Collections.Generic;
using SavingsManager.Data;

namespace SavingsManager.Factory
{
    public interface ISavingsProvider
    {
        SavingsDataRepository SavingsDataRepository { get; set; }
        IEnumerable<object> GetAllObjects();
        object GetObjectById(int id);
        void AddObject(object item);
        void UpdateObject(object item);
        void DeleteObject(object item);

    }
}
