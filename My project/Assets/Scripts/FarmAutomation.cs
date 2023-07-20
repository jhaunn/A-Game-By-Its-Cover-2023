using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmAutomation : MonoBehaviour
{
    private FarmPlot[] farmPlots;

    [SerializeField] private GameObject farmNPC;
    private TextMeshPro npcText;
    [SerializeField] private float npcInteractRadius;
    [SerializeField] private LayerMask playerLayerMask;

    private void Awake()
    {
        farmPlots = transform.GetComponentsInChildren<FarmPlot>();
        npcText = farmNPC.GetComponentInChildren<TextMeshPro>();
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

        if (Physics2D.OverlapCircle(farmNPC.transform.position, npcInteractRadius, playerLayerMask))
        {
            npcText.gameObject.SetActive(true);
            npcText.text = "Interact";

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacted with NPC");
            }
        }
        else
        {
            npcText.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(farmNPC.transform.position, npcInteractRadius);
    }
}
