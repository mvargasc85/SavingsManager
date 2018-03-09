using SavingsManager.Providers;

namespace SavingsManager.Factory
{
    public class SavingsProviderFactory
    {
        public static ISavingsProvider CreateSavingsModelObject(string type)
        {
            switch (type)
            {
                case "Grupo":
                    return new GroupProvider();
                case "Plan":
                    return new PlanProvider();
                default:
                    return null;
            }
        }
    }
}