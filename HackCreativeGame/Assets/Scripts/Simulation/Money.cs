using Game.Simulation;
using System;
using UnityEngine;

namespace Assets.Scripts.Simulation
{
    public class Money
    {
        public float Amount { get; private set; } = 1000;

        public float CalculateBenefitTax()
        {
            var amount = 0f;
            var benefits = SimulationController.Instance.benefits;
            foreach (var item in benefits.Get())
            {
                var pricePerHealthy = item.PricePerHealthy 
                    * (SimulationController.Instance.statistics.TotalHealthy 
                        + SimulationController.Instance.statistics.TotalRecovered);
                var pricePerSick = item.PricePerKnownSick * SimulationController.Instance.statistics.TotalSymptomatic;
                amount -= (pricePerHealthy + pricePerSick);
            }
            return amount;
        }

        public float CalculateWorkFlowTax()
        {
            var amount = 0f;
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
                    var negative = population.Settings.TaxPerTurnSymptomatic
                        * (population.population.symptomatic);
                    amount += positive + negative;
                }
            }
            return amount;
        }

        internal void CalculateTax()
        {
            Amount += CalculateBenefitTax();
            Amount += CalculateWorkFlowTax();
        }

        public float CalculateFutureSpending()
        {
            return CalculateBenefitTax() + CalculateWorkFlowTax();
        }
    }
}
