using System;
using System.Collections;
using System.Collections.Generic;
using Game.Simulation;
using UnityEngine;

public class MapRegionEffects : MonoBehaviour
{
    private RegionNode _regionNode;
    private SpriteRenderer _mapRegionSprite;

    private void Awake()
    {
        _regionNode = GetComponent<RegionNode>();
        _mapRegionSprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateRegionSprite()
    {
        _mapRegionSprite.color = Color.Lerp(Color.white, Color.red,
            _regionNode.TotalPopulation.symptomatic / (float) _regionNode.TotalPopulation.Total);
        //_mapRegionSprite.color = Color.Lerp(_mapRegionSprite.color, Color.black,
        //    1f - (_regionNode.TotalPopulation.Total / (float) _regionNode.settings.initialPopulation));
        //This does not work because of travel.
    }
}
