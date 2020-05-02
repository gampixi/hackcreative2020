using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using TMPro;
using UnityEngine;
using static Game.Simulation.SimulationController;

public class UiMainHud : MonoBehaviour
{
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI populationText;

    private void Update()
    {
        happinessText.text = $"HAPPINESS: {Instance.happiness.happiness}";
        moneyText.text = $"{Instance.money.Amount}€";
        populationText.text = $"Alive: {Instance.statistics.TotalAlive} Inf: {Instance.statistics.TotalSymptomatic} Dead: {Instance.statistics.TotalDead}";
    }

    public void NextDayButton()
    {
        Instance.TickForward();
    }
}
