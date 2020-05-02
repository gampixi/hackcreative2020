using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Population
{
    public int healthy;
    public int symptomatic;
    public int asymptomatic;
    public int recovered;
    public long Total => healthy + symptomatic + asymptomatic + recovered;

    public static Population operator+(Population a, Population b)
    {
        a.healthy += b.healthy;
        a.symptomatic += b.symptomatic;
        a.asymptomatic += b.asymptomatic;
        a.recovered += b.recovered;
        return a;
    }
    
    public static Population operator-(Population a, Population b)
    {
        a.healthy -= b.healthy;
        a.symptomatic -= b.symptomatic;
        a.asymptomatic -= b.asymptomatic;
        a.recovered -= b.recovered;
        return a;
    }
    
    public static Population operator*(Population a, float b)
    {
        a.healthy = Mathf.RoundToInt(a.healthy * b);
        a.symptomatic = Mathf.RoundToInt(a.symptomatic * b);
        a.asymptomatic = Mathf.RoundToInt(a.asymptomatic * b);
        a.recovered = Mathf.RoundToInt(a.recovered * b);
        return a;
    }

    public void Reset()
    {
        healthy = 0;
        symptomatic = 0;
        asymptomatic = 0;
        recovered = 0;
    }

    public override string ToString()
    {
        return $"{nameof(healthy)}: {healthy}, {nameof(symptomatic)}: {symptomatic}, {nameof(asymptomatic)}: {asymptomatic}, {nameof(recovered)}: {recovered}";
    }
}
