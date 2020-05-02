using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using TMPro;
using UnityEngine;

public class UiMainHud : MonoBehaviour
{
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI moneyText;

    private void Update()
    {
        happinessText.text = $"HAPPINESS: {SimulationController.Instance.happiness.happiness}";
    }

    public void NextDayButton()
    {
        SimulationController.Instance.TickForward();
    }
}
