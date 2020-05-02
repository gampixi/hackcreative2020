using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using UnityEngine;

public class Happiness : MonoBehaviour
{
    public double happiness { get; private set; }

    public void CalculateHappiness()
    {
        happiness = 0;
        long totalPopulation = 0;
        SimulationController.Instance.regions.ForEach(x => x.population.ForEach(p =>
        {
            happiness += p.GetHappiness();
            totalPopulation += p.population.Total;
        }));
        happiness /= (double) totalPopulation;
        if (happiness > 1.0)
            happiness = 1.0;
        else if (happiness < -1.0)
            happiness = -1.0;
    }
}
