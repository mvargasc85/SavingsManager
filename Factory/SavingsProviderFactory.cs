using SavingsManager.Data;
using SavingsManager.Providers;

namespace SavingsManager.Factory
{
    /// <summary>
    /// Class to manage the creation of objects that implements ISavingsProvider interface:
    /// GroupProvider, PlanProvider, SocioProvider, etc
    /// Implements the Factory and Singleton and dependency injection patterns
    /// </summary>
    public class SavingsProviderFactory
    {

        private static SavingsProviderFactory instance;
        SavingsDataRepository savingsDataRepository;
        private SavingsProviderFactory()
        {
            savingsDataRepository = new SavingsDataRepository();
        }

        /// <summary>
        /// Returns the instance of SavingsProviderFactory guarranting that there will only one instance at time
        /// </summary>
        public static SavingsProviderFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SavingsProviderFactory();
                }
                return instance;
            }
        }




        /// <summary>
        /// Creates a new object of type provinding a reference of SavingsDataRepository in order to implement 
        /// dependency injection pattern.
        /// </summary>
        /// <param name="type">indicates the type of the object to create</param>
        /// <returns>a IsavingsProvider object according to the type passed by parameter</returns>
        public ISavingsProvider CreateSavingsModelObject(string type)
        {
            savingsDataRepository =   new SavingsDataRepository();
            switch (type)
            {
                case "Grupo":
                    return new GroupProvider(savingsDataRepository);
                case "Socio":
                    return new SocioProvider(savingsDataRepository);
                case "Plan":
                    return new PlanProvider(savingsDataRepository);
                case "Ahorro":
                    return new AhorroProvider(savingsDataRepository);
                default:
                    return null;
            }
        }
    }
}