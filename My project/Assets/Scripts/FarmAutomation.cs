using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    private FarmPlot[] farmPlots;

    private TextMeshPro npcText;
    [SerializeField] private float npcInteractRadius;
    [SerializeField] private LayerMask playerLayerMask;

    private void Awake()
    {
        farmPlots = transform.GetComponentsInChildren<FarmPlot>();
    }

    private void Update()
    {
        for (int i = 0; i < farmPlots.Length; i++)
        {
            if (farmPlots[i].GetIsAutomated())
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
}
