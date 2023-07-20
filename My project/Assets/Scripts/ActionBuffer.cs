using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBuffer : MonoBehaviour
{
    public static ActionBuffer instance;

    [SerializeField] private float plantBuffer;
    [SerializeField] private float harvestBuffer;
    private float currentPlantBuffer = 0f;
    private float currentHarvestBuffer = 0f;

    [SerializeField] private Slider plantSlider;
    [SerializeField] private Slider harvestSlider;

    [HideInInspector] public bool canPlant = false;
    [HideInInspector] public bool canHarvest = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        plantSlider.maxValue = plantBuffer;
        harvestSlider.maxValue = harvestBuffer;
    }

    private void Update()
    {
        plantSlider.value = currentPlantBuffer;
        harvestSlider.value = currentHarvestBuffer;

        currentPlantBuffer += Time.deltaTime;
        currentHarvestBuffer += Time.deltaTime;

        if (currentPlantBuffer >= plantSlider.maxValue)
        {
            canPlant = true;
        }
        else
        {
            canPlant = false;
        }

        if (currentHarvestBuffer >= harvestSlider.maxValue)
        {
            canHarvest = true;
        }
        else
        {
            canHarvest = false;
        }
    }

    public void UsePlantAction()
    {
        currentPlantBuffer = 0f;
    }

    public void UseHarvestAction()
    {
        currentHarvestBuffer = 0f;
    }
}
