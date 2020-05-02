using Game.Simulation;

namespace Assets.Scripts.Simulation
{
    public class Money
    {
        public float Amount { get; private set; } = 1000;

        private SimulationController simulationController = SimulationController.Instance;
        public void CalculateBenefitTax()
        {
            var benefits = simulationController.benefits;
            foreach (var item in benefits.Get())
            {
                var pricePerHealthy = item.PricePerHealthy 
                    * (simulationController.statistics.TotalHealthy 
                        + simulationController.statistics.TotalRecovered);
                var pricePerSick = item.PricePerKnownSick * simulationController.statistics.TotalSymptomatic;
                Amount -= (pricePerHealthy + pricePerSick);
            }
        }

        public void CalculateWorkFlowTax()
        {
            foreach (var regions in SimulationController.Instance.regions)
            {
                foreach (var population in regions.population)
                {
                    var positive = population.Settings.TaxPerTurn
                        * (population.population.healthy
                            + population.population.recovered
                            + population.population.asymptomatic);
                    if (population.Settings.kind == FlowGroupKind.Anarchist
                        || population.Settings.kind == FlowGroupKind.Unemployed)
                    {
                        positive *= -1;
                    }
                    var negative = population.Settings.TaxPerTurn
                        * (population.population.symptomatic) * -1;
                    Amount += positive + negative;
                }
            }
        }
    }
}
