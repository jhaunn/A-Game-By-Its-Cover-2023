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
    [SerializeField] private int currentUpgrade = 0;


    [Header("NPC Interaction")]
    [SerializeField] private Transform npc;
    [SerializeField] private TextMeshPro npcText;
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("UI")]
    [SerializeField] private GameObject panel;
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

            if (isAutomated)
            {
                if (currentUpgrade == 0)
                {
                    panelText.text = $"Farm Stats \n Growth: {farmStat.minGrowthSpeed}x - {farmStat.maxGrowthSpeed}x \n" +
                        $"Yield: {farmStat.minYield} - {farmStat.maxYield} \n\n" +
                        $"Upgrade for {farmStat.upgradePrice[currentUpgrade]} \n Press E to Confirm";
                }
                else if (currentUpgrade > 0)
                {
                    panelText.text = $"Farm Stats \n Growth: {farmStat.upgradeMinGrowthSpeed[currentUpgrade - 1]}x - " +
                        $"{farmStat.upgradeMaxGrowthSpeed[currentUpgrade - 1]}x \n" +
                        $"Yield: {farmStat.upgradeMinYield[currentUpgrade - 1]} - {farmStat.upgradeMaxYield[currentUpgrade - 1]} \n\n" +
                        $"Upgrade for {farmStat.upgradePrice[currentUpgrade]} \n Press E to Confirm";
                }

                if (Input.GetKeyDown(KeyCode.E) && ScoreManager.instance.GetResource() >= farmStat.upgradePrice[currentUpgrade] && 
                    currentUpgrade < farmStat.upgradePrice.Length)
                {
                    ScoreManager.instance.RemoveResource(farmStat.upgradePrice[currentUpgrade]);
                    currentUpgrade++;
                }
            }

            else
            {
                panelText.text = $"Unlock for {farmStat.farmPrice} \n Press E to confirm";

                if (Input.GetKeyDown(KeyCode.E) && panel.activeSelf && ScoreManager.instance.GetResource() >= farmStat.farmPrice)
                {
                    UnlockFarm();
                }
            }

            panel.SetActive(true);
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

    public int GetCurrentUpgrade()
    {
        return currentUpgrade;
    }

    private void UnlockFarm()
    {
        isAutomated = true;
        ScoreManager.instance.RemoveResource(farmStat.farmPrice);
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
