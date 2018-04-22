using SavingsManager.Data;
using SavingsManager.Providers;

namespace SavingsManager.Factory
{
    public class SavingsProviderFactory
    {
        SavingsDataRepository savingsDataRepository = new SavingsDataRepository();

        public static ISavingsProvider CreateSavingsModelObject(string type)
        {
            
            switch (type)
            {
                case "Grupo":
                    return new GroupProvider();
                case "Socio":
                    return new SocioProvider();
                case "Plan":
                    return new PlanProvider();
                case "Ahorro":
                    return new AhorroProvider();
                default:
                    return null;
            }
        }
    }
}