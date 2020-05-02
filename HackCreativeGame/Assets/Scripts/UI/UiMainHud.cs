using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Game.Simulation.SimulationController;

public class UiMainHud : MonoBehaviour
{
    public Transform happiness;
    public Image happinessImage;
    public Color negativeColor;
    public Color positiveColor;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyChangeText;
    public TextMeshProUGUI aliveText;
    public TextMeshProUGUI infectedText;
    public TextMeshProUGUI deadText;

    private void Update()
    {
        happiness.localScale = new Vector3(Mathf.Clamp((float)Instance.happiness.happiness, -1f, 1f), 1, 1);
        happinessImage.color = Instance.happiness.happiness < 0 ? positiveColor : negativeColor;
        moneyText.text = Instance.money.Amount.ToString("N0");
        moneyChangeText.text = $"aaaa1";
        aliveText.text = Instance.statistics.TotalAlive.ToString();
        infectedText.text = Instance.statistics.TotalSymptomatic.ToString();
        deadText.text = Instance.statistics.TotalDead.ToString();
    }

    public void NextDayButton()
    {
        Instance.TickForward();
    }
}
