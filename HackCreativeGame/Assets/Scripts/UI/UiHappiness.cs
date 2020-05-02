using System;
using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using UnityEngine;
using TMPro;

public class UiHappiness : MonoBehaviour
{
    public TextMeshProUGUI happinessText;

    private void Update()
    {
        happinessText.text = $"HAPPINESS: {SimulationController.Instance.happiness.happiness}";
    }
}
