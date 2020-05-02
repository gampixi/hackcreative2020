using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewBenefitSettings", menuName = "Custom Game Data/Benefit Settings", order = 1)]
    public partial class Benefit : ScriptableObject
    {
        public List<FlowGroupData> FlowData;
    }
}