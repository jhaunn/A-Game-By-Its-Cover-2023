using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeralityManager : MonoBehaviour
{
    public static FeralityManager instance;

    [Header("Ferality Stats")]
    [SerializeField] private float maxFerality;
    [SerializeField] private float feralityMultiplier = 1f;

    [Header("Player Interaction")]
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("UI")]
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshPro text;

    private int requiredFeed;

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
        slider.maxValue = maxFerality;
        slider.value = slider.maxValue;
    }

    private void Update()
    {
        requiredFeed = (int)slider.maxValue - (int)slider.value;

        if (slider.value > 0f)
        {
            slider.value -= Time.deltaTime * feralityMultiplier;
        }

        if (Physics2D.OverlapCircle(transform.position, interactRadius, playerLayerMask))
        {
            text.gameObject.SetActive(true);

            text.text = $"Replenish for {requiredFeed}";

            if (Input.GetKeyDown(KeyCode.E) && ScoreManager.instance.GetResource() >= requiredFeed)
            {
                slider.value = slider.maxValue;
                ScoreManager.instance.RemoveResource(requiredFeed);
            }
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
