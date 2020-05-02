using System;
using Game;
using Game.Simulation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BenefitProvider : MonoBehaviour
{
    [SerializeField]
    private List<Benefit> Benefits = new List<Benefit>();
    private List<FlowGroupData> FlowGroupsData = new List<FlowGroupData>();

    public List<Benefit> Get()
    {
        return Benefits;
    }

    public void Add(Benefit benefit)
    {
        Benefits.Add(benefit);
        CalculateFlowData();
    }
    
    public void Remove(Benefit benefit)
    {
        var result = Benefits.Remove(benefit);
        Debug.Log($"Removing {benefit.Title} result {result}");
        CalculateFlowData();
    }

    public bool Active(Benefit benefit) => Benefits.Contains(benefit);

    public void CalculateFlowData()
    {
        FlowGroupsData.ForEach(x => x.Reset());
        foreach (var item in Benefits)
        {
            foreach (var data in item.FlowData)
            {
                var flowGroupElement = GetOrSetFlowData(data.target);
                flowGroupElement.happinessMultiplier *= data.happinessMultiplier;
                flowGroupElement.infectProbability *= data.infectProbability;
                data.transmitMultipliers.ForEach(x => flowGroupElement.transmitMultipliers.FirstOrDefault(y => y.target == x.target).transmitMultiplier *= x.transmitMultiplier);
            }
        }
    }

    public FlowGroupData GetOrSetFlowData(FlowGroupKind flowGroupKind)
    {
        var flowGroupElement = FlowGroupsData
                      .FirstOrDefault(x => x.target == flowGroupKind);
        if (flowGroupElement == null)
        {
            flowGroupElement = new FlowGroupData(flowGroupKind);
            FlowGroupsData.Add(flowGroupElement);
        }

        return flowGroupElement;
    }

}


