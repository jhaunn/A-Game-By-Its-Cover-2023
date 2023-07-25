using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    [SerializeField] private FarmStatSO farmStat;
    private FarmPlot[] farmPlots;

    [SerializeField] private bool isAutomated = false;

    [SerializeField] private Transform npc;
    [SerializeField] private TextMeshPro npcText;
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private LayerMask playerLayerMask;

    [SerializeField] private GameObject panel;
    private GameObject currentFarm;
    private TextMeshProUGUI panelText;

    private void Awake()
    {
        farmPlots = transform.GetComponentsInChildren<FarmPlot>();
        panelText = panel.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isAutomated)
        {
            AutomateFarm();
        }

        if (Physics2D.OverlapCircle(npc.position, interactRadius, playerLayerMask))
        {
            npcText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                panel.SetActive(true);

                if (isAutomated)
                {
                    panelText.text = $"Farm Stats \n Growth: {farmStat.minGrowthSpeed}x- {farmStat.maxGrowthSpeed}x \n" +
                        $"Yield: {farmStat.minYield} - {farmStat.maxYield}";
                }
                else
                {
                    panelText.text = "Unlock for 10000 \n Press E to confirm";
                }
                
            }
        }
        else
        {
            npcText.gameObject.SetActive(false);
            panel.SetActive(false);
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(npc.position, interactRadius);
    }
}
