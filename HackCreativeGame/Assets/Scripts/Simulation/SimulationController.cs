using Assets.Scripts.Simulation;
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
        public static SimulationController Instance;
        public GameDatabase data;
        public List<RegionNode> regions;
        public BenefitProvider benefits;
        public Happiness happiness;
        public Money money = new Money();
        public Statistics statistics = new Statistics();
        public SimulationGraphicsController graphics;

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

        private bool _firstTick = false;
        private void LateUpdate()
        {
            if (!_firstTick)
            {
                //Set infection region zero
                StartInfection();
                TickForward();
                _firstTick = true;
            }
        }

        public void StartInfection()
        {
            var region = regions[Random.Range(0, regions.Count)];
            region.InfectRandomly(10);
            Debug.Log($"Infection starts in {region.regionName}");
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
            statistics.Calculate();
            graphics.UpdateGraphics();
            money.CalculateTax();
        }
    }
}

