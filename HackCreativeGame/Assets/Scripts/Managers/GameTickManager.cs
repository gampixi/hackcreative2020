using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Simulation
{
    public class GameTickManager : MonoBehaviour
    {
        public float tickTime = 1;
        private float _nextTickTime = 1;
        
        private void Update()
        {
            if (Time.time > _nextTickTime)
            {
                SimulationController.Instance.TickForward();
                _nextTickTime += tickTime;
            }
        }
    }
}