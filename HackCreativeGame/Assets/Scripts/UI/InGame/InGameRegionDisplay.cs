using System;
using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using UnityEngine;
using TMPro;

namespace Game.Interface
{
    public class InGameRegionDisplay : MonoBehaviour
    {
        public RegionNode link;
        public TextMeshPro debugText;

        private void Update()
        {
            debugText.text = link.TotalPopulation.ToString();
        }
    }
}