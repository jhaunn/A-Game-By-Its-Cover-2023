using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private float interactRadius = 0.25f;
    [SerializeField] private LayerMask playerLayerMask;

    [SerializeField] private TextMeshPro text;

    [SerializeField] private PlantsSO plant;
    private Sprite tilledDirt;

    private bool isPlanted = false;

    private float cropGrowth = 0f;
    private float growthMultiplier;

    private bool isGrown = false;

    private void Start()
    {
        tilledDirt = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (isPlanted && !isGrown)
        {
            GrowCrop();
        }
    }

    public bool GetIsPlanted()
    {
        return isPlanted;
    }

    public bool GetIsGrown()
    {
        return isGrown;
    }

    public void PlantCrop()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = plant.growth[0];
        growthMultiplier = Random.Range(plant.minGrowthSpeed, plant.maxGrowthSpeed);
        isPlanted = true;
    }

    private void GrowCrop()
    {
        text.gameObject.SetActive(true);
        text.text = cropGrowth.ToString("0") + "%";

        if (cropGrowth < 100f)
        {
            cropGrowth += Time.deltaTime * growthMultiplier;
        }

        if (cropGrowth >= 33.3f)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = plant.growth[1];
        }
        if (cropGrowth >= 66.6f)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = plant.growth[2];
        }
        if (cropGrowth >= 100f)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = plant.growth[3];
            isGrown = true;
        }
    }

    public void HarvestCrop()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = tilledDirt;
        ScoreManager.instance.AddResource(Random.Range(plant.minYield, plant.maxYield));

        isPlanted = false;
        isGrown = false;
        cropGrowth = 0f;
        growthMultiplier = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
