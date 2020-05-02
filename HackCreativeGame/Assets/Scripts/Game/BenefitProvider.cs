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
    public void Add(Benefit benefit)
    {
        Benefits.Add(benefit);
    }

    private List<FlowGroupData> FlowGroupsData = new List<FlowGroupData>();

    private void Start()
    {
        // Initialize flowgroupsdata as an empty thingy
        foreach (FlowGroupKind kind in (FlowGroupKind[]) Enum.GetValues(typeof(FlowGroupKind)))
        {
            var flowGroupElement = new FlowGroupData(kind);
            FlowGroupsData.Add(flowGroupElement);
        }
    }

    public void CalculateFlowData()
    {
        foreach (var item in Benefits)
        {
            foreach (var data in item.FlowData)
            {
                var flowGroupElement = FlowGroupsData
                    .FirstOrDefault(x => x.target == data.target);
                if (flowGroupElement == null)
                {
                    flowGroupElement = new FlowGroupData(data.target);
                    FlowGroupsData.Add(flowGroupElement);
                }
                flowGroupElement.Reset();
                flowGroupElement.happinessMultiplier *= data.happinessMultiplier;
                flowGroupElement.infectProbability *= data.infectProbability;
            }
        }
    }

    public FlowGroupData GetDataForKind(FlowGroupKind kind)
    {
        return FlowGroupsData.FirstOrDefault(x => x.target == kind);
    }
}


