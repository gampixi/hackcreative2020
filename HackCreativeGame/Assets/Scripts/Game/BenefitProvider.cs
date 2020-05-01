using Game;
using Game.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenefitProvider : MonoBehaviour
{
    private List<Benefit> Benefits;
    public void Add(Benefit benefit)
    {
        Benefits.Add(benefit);
    }
    [System.Serializable]
    public class FlowGroupData
    {
        public FlowGroupKind target;
        public float infectProbability;
        public float happinessMultiplier;
    }

    public List<FlowGroupData> flowGroupData;
    
    public void CalculateFlowData()
    {
        foreach (var item in Benefits)
        {

        }
    }
}

    
