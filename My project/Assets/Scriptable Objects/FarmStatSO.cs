using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Farm Stat", menuName = "Farm Stat")]
public class FarmStatSO : ScriptableObject
{
    public PlantsSO plant;

    public float minGrowthSpeed, maxGrowthSpeed;
    public int minYield, maxYield;
}
