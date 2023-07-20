using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    private FarmPlot[] farmPlots;

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
