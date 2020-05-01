using Game;
using Game.Simulation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Game.Benefit;

public class BenefitProvider : MonoBehaviour
{
    private List<Benefit> Benefits;
    public void Add(Benefit benefit)
    {
        Benefits.Add(benefit);
    }

    public List<FlowGroupData> FlowGroupsData = new List<FlowGroupData>();

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
                flowGroupElement.happinessMultiplier *= data.happinessMultiplier;
                flowGroupElement.infectProbability *= data.infectProbability;
            }
        }
    }
}


