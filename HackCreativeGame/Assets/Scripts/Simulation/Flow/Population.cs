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

    public static Population operator +(Population a, Population b)
    {
        a.healthy += b.healthy;
        a.symptomatic += b.symptomatic;
        a.asymptomatic += b.asymptomatic;
        a.recovered += b.recovered;
        return a;
    }
}
