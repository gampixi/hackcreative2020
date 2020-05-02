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

    public void Add(Benefit benefit)
    {
        Benefits.Add(benefit);
    }

    public void CalculateFlowData()
    {
        foreach (var item in Benefits)
        {
            foreach (var data in item.FlowData)
            {
                var flowGroupElement = GetOrSetFlowData(data.target);
                flowGroupElement.happinessMultiplier *= data.happinessMultiplier;
                flowGroupElement.infectProbability *= data.infectProbability;
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


