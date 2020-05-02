using System;
using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using UnityEngine;
using UnityEngine.UI;

public class UiBenefitWindow : MonoBehaviour
{
    public GameObject listItemPrefab;
    private List<GameObject> _currentList = new List<GameObject>();

    public Transform activeList;
    public Transform inactiveList;

    private void Start()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        _currentList.ForEach(Destroy);
        _currentList.Clear();
        SimulationController.Instance.data.benefits.ForEach(x =>
        {
            var act = SimulationController.Instance.benefits.Active(x);
            var item = Instantiate(listItemPrefab, act ? activeList : inactiveList, false);
            var frag = item.GetComponent<UiBenefitListFragment>();
            frag.Initialize(x, this, act);
        });
    }

    public void MoveToActive(Transform t)
    {
        t.SetParent(activeList, false);
    }
    
    public void MoveToInactive(Transform t)
    {
        t.SetParent(inactiveList, false);
    }
}
