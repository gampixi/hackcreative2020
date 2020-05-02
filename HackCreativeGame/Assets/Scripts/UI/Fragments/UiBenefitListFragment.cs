using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Simulation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiBenefitListFragment : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI price;
    
    public GameObject buyButton;
    public GameObject cancelButton;

    private Benefit _linkedBenefit;
    private UiBenefitWindow _window;

    public void Initialize(Benefit benefit, UiBenefitWindow window, bool active)
    {
        _window = window;
        _linkedBenefit = benefit;

        title.text = benefit.Title;
        description.text = benefit.Description;
        price.text = $"{benefit.PricePerHealthy}€ healthy, {benefit.PricePerKnownSick}€ sick, per capita/day";
        
        buyButton.SetActive(!active);
        cancelButton.SetActive(active);
    }

    public void ActivateBenefit()
    {
        if (SimulationController.Instance.benefits.Active(_linkedBenefit)) return;
        SimulationController.Instance.benefits.Add(_linkedBenefit);
        
        buyButton.SetActive(false);
        cancelButton.SetActive(true);
        _window.MoveToActive(transform);
    }
    
    public void DeactivateBenefit()
    {
        if (!SimulationController.Instance.benefits.Active(_linkedBenefit)) return;
        SimulationController.Instance.benefits.Remove(_linkedBenefit);
        
        buyButton.SetActive(true);
        cancelButton.SetActive(false);
        _window.MoveToInactive(transform);
    }
}
