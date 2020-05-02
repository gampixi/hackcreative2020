using Game.Simulation;
using System;
using UnityEngine;

namespace Assets.Scripts.Simulation
{
    public class Money
    {
        public float Amount { get; private set; } = 1000;

        public void CalculateBenefitTax()
        {
            var benefits = SimulationController.Instance.benefits;
            foreach (var item in benefits.Get())
            {
                var pricePerHealthy = item.PricePerHealthy 
                    * (SimulationController.Instance.statistics.TotalHealthy 
                        + SimulationController.Instance.statistics.TotalRecovered);
                var pricePerSick = item.PricePerKnownSick * SimulationController.Instance.statistics.TotalSymptomatic;
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

        internal void CalculateTax()
        {
            CalculateBenefitTax();
            CalculateWorkFlowTax();
        }
    }
}
