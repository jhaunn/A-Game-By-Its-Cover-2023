using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    private FarmStatSO farmStat;
    private PlantsSO plant;
    private Sprite tilledDirt;

    private bool isPlanted = false;

    private float cropGrowth = 0f;
    private float growthMultiplier;

    private bool isGrown = false;

    private AudioSource soundFx;

    private void Awake()
    {
        farmStat = GetComponentInParent<FarmAutomation>().GetFarmStat();

        soundFx = SoundManager.instance.SetupAudio(gameObject);
    }

    private void Start()
    {
        tilledDirt = GetComponentInChildren<SpriteRenderer>().sprite;
        plant = farmStat.plant;


        text.text = "";
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

        int currentUpgrade = GetComponentInParent<FarmAutomation>().GetCurrentUpgrade();

        if (currentUpgrade == 0)
        {
            growthMultiplier = Random.Range(farmStat.minGrowthSpeed, farmStat.maxGrowthSpeed);
        }
        else if (currentUpgrade > 0)
        {
            growthMultiplier = Random.Range(farmStat.upgradeMinGrowthSpeed[currentUpgrade - 1], farmStat.upgradeMaxGrowthSpeed[currentUpgrade - 1]);
        }

        isPlanted = true;

        Instantiate(EffectsManager.instance.particles[0], transform.position, EffectsManager.instance.particles[0].transform.rotation);

        soundFx.PlayOneShot(SoundManager.instance.sounds[1]);
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

        int currentUpgrade = GetComponentInParent<FarmAutomation>().GetCurrentUpgrade();

        if (currentUpgrade == 0)
        {
            ScoreManager.instance.AddResource(Random.Range(farmStat.minYield, farmStat.maxYield));
        }
        else if (currentUpgrade > 0)
        {
            ScoreManager.instance.AddResource(Random.Range(farmStat.upgradeMinYield[currentUpgrade - 1], farmStat.upgradeMaxYield[currentUpgrade - 1]));
        }

        isPlanted = false;
        isGrown = false;
        cropGrowth = 0f;
        growthMultiplier = 0f;

        Instantiate(EffectsManager.instance.particles[1], transform.position, EffectsManager.instance.particles[1].transform.rotation);

        soundFx.PlayOneShot(SoundManager.instance.sounds[0]);

    }
}
