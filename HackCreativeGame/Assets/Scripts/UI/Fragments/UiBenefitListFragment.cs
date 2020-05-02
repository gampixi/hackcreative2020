using System.Collections;
using System.Collections.Generic;
using Game;
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

    public void Initialize(Benefit benefit, UiBenefitWindow window)
    {
        _window = window;
        _linkedBenefit = benefit;
        
        
    }
    
    
}
