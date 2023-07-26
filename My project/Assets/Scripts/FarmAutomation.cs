using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    private FarmPlot[] farmPlots;

    [Header("Farm Stat")]
    [SerializeField] private FarmStatSO farmStat;
    [SerializeField] private bool isAutomated = false;


    [Header("NPC Interaction")]
    [SerializeField] private Transform npc;
    [SerializeField] private TextMeshPro npcText;
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("UI")]
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
                if (isAutomated)
                {
                    panelText.text = $"Farm Stats \n Growth: {farmStat.minGrowthSpeed}x- {farmStat.maxGrowthSpeed}x \n" +
                        $"Yield: {farmStat.minYield} - {farmStat.maxYield}";
                }
                else
                {
                    panelText.text = $"Unlock for {farmStat.farmPrice} \n Press E to confirm";

                    if (Input.GetKeyDown(KeyCode.E) && panel.activeSelf && ScoreManager.instance.GetResource() >= farmStat.farmPrice)
                    {
                        isAutomated = true;

                        panelText.text = $"Farm Stats \n Growth: {farmStat.minGrowthSpeed}x- {farmStat.maxGrowthSpeed}x \n" +
                            $"Yield: {farmStat.minYield} - {farmStat.maxYield}";
                    }
                }

                panel.SetActive(true);
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
