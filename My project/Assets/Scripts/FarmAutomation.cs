using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    private FarmPlot[] farmPlots;

    [SerializeField] private bool automatePlant = false;
    [SerializeField] private bool automateHarvest = false;

    private void Awake()
    {
        farmPlots = transform.GetComponentsInChildren<FarmPlot>();
    }

    private void Update()
    {
        AutomateFarm();
    }

    private void AutomateFarm()
    {
        for (int i = 0; i < farmPlots.Length; i++)
        {
            if (!farmPlots[i].GetIsPlanted() && automatePlant)
            {
                farmPlots[i].PlantCrop();
            }

            if (farmPlots[i].GetIsGrown() && automateHarvest)
            {
                farmPlots[i].HarvestCrop();
            }
        }
    }
}
