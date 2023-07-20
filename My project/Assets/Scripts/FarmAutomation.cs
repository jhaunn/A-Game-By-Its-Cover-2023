using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    [SerializeField] private FarmStatSO farmStat;
    private FarmPlot[] farmPlots;

    private void Awake()
    {
        farmPlots = transform.GetComponentsInChildren<FarmPlot>();
    }

    private void Update()
    {
        AutomateFarm();
    }

    public FarmStatSO GetFarmStat()
    {
        return farmStat;
    }

    private void AutomateFarm()
    {
        for (int i = 0; i < farmPlots.Length; i++)
        {
            if (!farmPlots[i].GetIsPlanted())
            {
                farmPlots[i].PlantCrop();
            }

            if (farmPlots[i].GetIsGrown())
            {
                farmPlots[i].HarvestCrop();
            }
        }
    }
}
