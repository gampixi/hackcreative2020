using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Simulation
{
    public class SimulationController : MonoBehaviour
    {
        private PopulationStatistics statistics = new PopulationStatistics();
        public static SimulationController Instance;
        public GameDatabase data;
        public List<RegionNode> regions;
        public BenefitProvider benefits;
        public Happiness happiness;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Random.InitState(Guid.NewGuid().GetHashCode());
            regions.ForEach(x => x.InitializeRegion());
        }

        [ContextMenu("Tick Forward")]
        public void TickForward()
        {
            // Calculations should be done before actually manipulating the data
            // so no weird side effects happen
            // Order should be kept: healing -> internal transmission -> travel
            regions.ForEach(x => {
                x.population.ForEach(p => p.PerformInternalChanges());
                x.PerformInternalTransmission();
                x.PerformInternalFlowChange();
                x.PerformTravel();
            });
            
            regions.ForEach(x =>
            {
                x.UpdateTotalPopulation();
            });
            happiness.CalculateHappiness();
        }

        public class PopulationStatistics : MonoBehaviour
        {
            public int TotalRecovered { get; set; }
            public int TotalHealthy { get; set; }
            public int TotalSymptomatic { get; set; }
            public long TotalAlive => TotalHealthy + TotalSymptomatic + TotalRecovered;
            public long TotalDead { get; set; }
        }

        public PopulationStatistics GetStatistics()
        {
            statistics.TotalHealthy = regions.Sum(region => region
                .population.Sum(people => people.population.healthy + people.population.asymptomatic));

            statistics.TotalSymptomatic = regions.Sum(region => region
               .population.Sum(people => people.population.symptomatic));

            statistics.TotalRecovered = regions.Sum(region => region
               .population.Sum(people => people.population.recovered));

            var totalInitial = regions.Sum(x => x.settings.initialPopulation);

            statistics.TotalDead = totalInitial - statistics.TotalAlive;

            return statistics;
        }

    }
}

