using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameDatabase", menuName = "Custom Game Data/Database", order = 1)]
public class GameDatabase : ScriptableObject
{
    public List<Benefit> benefits;
    public List<Benefit> orders;
}
