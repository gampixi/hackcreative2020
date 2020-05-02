using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiOrderListFragment : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public GameObject buyButton;
    public GameObject cancelButton;

    private Benefit _linkedBenefit;
    private UiOrderWindow _window;

    public void Initialize(Benefit benefit, UiOrderWindow window, bool active)
    {
        _window = window;
        _linkedBenefit = benefit;

        title.text = benefit.Title;
        description.text = benefit.Description;
        
        buyButton.SetActive(!active);
        cancelButton.SetActive(active);
    }

    public void ActivateOrder()
    {
        if (SimulationController.Instance.benefits.Active(_linkedBenefit)) return;
        SimulationController.Instance.benefits.Add(_linkedBenefit);
        
        buyButton.SetActive(false);
        cancelButton.SetActive(true);
        _window.MoveToActive(transform);
    }
    
    public void DeactivateOrder()
    {
        if (!SimulationController.Instance.benefits.Active(_linkedBenefit)) return;
        SimulationController.Instance.benefits.Remove(_linkedBenefit);
        
        buyButton.SetActive(true);
        cancelButton.SetActive(false);
        _window.MoveToInactive(transform);
    }
}
