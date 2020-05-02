using Game.Simulation;
using System.Linq;
using UnityEngine;

public class Statistics
{
    public int TotalRecovered { get; set; }
    public int TotalHealthy { get; set; }
    public int TotalSymptomatic { get; set; }
    public long TotalAlive => TotalHealthy + TotalSymptomatic + TotalRecovered;
    public long TotalDead { get; set; }

    public void Calculate()
    {
        TotalHealthy = SimulationController.Instance.regions.Sum(region => region
                .population.Sum(people => people.population.healthy + people.population.asymptomatic));

        TotalSymptomatic = SimulationController.Instance.regions.Sum(region => region
           .population.Sum(people => people.population.symptomatic));

        TotalRecovered = SimulationController.Instance.regions.Sum(region => region
           .population.Sum(people => people.population.recovered));

        var totalInitial = SimulationController.Instance.regions.Sum(x => x.settings.initialPopulation);

        TotalDead = totalInitial - TotalAlive;
    }
}
