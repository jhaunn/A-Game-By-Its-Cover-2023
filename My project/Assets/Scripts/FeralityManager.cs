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

    private AudioSource soundFx;

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

        soundFx = SoundManager.instance.SetupAudio(gameObject);
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                ReplenishFerality();
            }
        }
        else
        {
            text.gameObject.SetActive(false);
        }

        if (slider.value <= 0f)
        {
            GameMenu.instance.GameOver();
        }
    }

    private void ReplenishFerality()
    {
        if (ScoreManager.instance.GetResource() > 0)
        {
            if (ScoreManager.instance.GetResource() >= requiredFeed)
            {
                slider.value = slider.maxValue;
                ScoreManager.instance.RemoveResource(requiredFeed);
            }
            else
            {
                slider.value += ScoreManager.instance.GetResource();
                ScoreManager.instance.RemoveResource(ScoreManager.instance.GetResource());
            }

            soundFx.PlayOneShot(SoundManager.instance.sounds[4]);

            Instantiate(EffectsManager.instance.particles[2], transform.position, EffectsManager.instance.particles[2].transform.rotation);
            slider.gameObject.GetComponentInParent<Animator>().Play("Pulse");
        }
        else
        {
            soundFx.PlayOneShot(SoundManager.instance.sounds[3]);
        }
    }

    public void AddFerality(float amount)
    {
        slider.value += amount;
        slider.gameObject.GetComponentInParent<Animator>().Play("Pulse");
    }

    public void removeFerality(float amount)
    {
        slider.value -= amount;
        slider.gameObject.GetComponentInParent<Animator>().Play("Pulse");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
