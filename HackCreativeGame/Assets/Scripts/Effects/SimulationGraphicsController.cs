using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationGraphicsController : MonoBehaviour
{
    public List<MapRegionEffects> regions;

    public void UpdateGraphics()
    {
        regions.ForEach(x => x.UpdateRegionSprite());
    }
}
