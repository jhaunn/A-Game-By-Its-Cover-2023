using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Farm Stat", menuName = "Farm Stat")]
public class FarmStatSO : ScriptableObject
{
    [Header("Unlock Stats")]
    public int farmPrice;
    public PlantsSO plant;

    [Header("Base Stats")]
    public float minGrowthSpeed, maxGrowthSpeed;
    public int minYield, maxYield;

    [Header("Upgrade Stats")]
    public int[] upgradePrice;
    public float[] upgradeMinGrowthSpeed, upgradeMaxGrowthSpeed;
    public int[] upgradeMinYield, upgradeMaxYield;

}
