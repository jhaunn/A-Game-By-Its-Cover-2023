using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VillageNpc : MonoBehaviour
{
    [SerializeField] private float waitTimeMin, waitTimeMax;
    private float currentWaitTime;
    private bool canInteract = true;

    private float interactRadius = 1.5f;
    private LayerMask playerLayerMask;
    
    private AudioSource soundFx;

    private void Start()
    {
        currentWaitTime = Random.Range(waitTimeMin, waitTimeMax);

        playerLayerMask = LayerMask.GetMask("Player");

        soundFx = SoundManager.instance.SetupAudio(gameObject);

        soundFx.PlayOneShot(SoundManager.instance.sounds[5]);
    }

    private void Update()
    {
        currentWaitTime -= Time.deltaTime;

        if (currentWaitTime <= 0f)
        {
            if (canInteract)
            {
                Ignored();
            }
        }

        if (Physics2D.OverlapCircle(transform.position, interactRadius, playerLayerMask))
        {
            if (canInteract)
            {
                GetComponentInChildren<TextMeshPro>().text = "Interact";
            }
            else
            {
                GetComponentInChildren<TextMeshPro>().text = "...";
            }

            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                Interact();
            }

        }
        else
        {
            if (canInteract)
            {
                GetComponentInChildren<TextMeshPro>().text = "!";
            }
            else
            {
                GetComponentInChildren<TextMeshPro>().text = "...";
            }
        }
    }

    private void Interact()
    {
        canInteract = false;
        soundFx.PlayOneShot(SoundManager.instance.sounds[4]);

        float random = Random.Range(0f, 100f);

        int[] amount = { 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 15, 15, 15, 25, 25, 50, 100 };
        int index = Random.Range(0, amount.Length);

        if (random <= 75f)
        {
            ScoreManager.instance.AddResource(amount[index]);
        }
        else
        {
            FeralityManager.instance.AddFerality(amount[index]);
        }

        Invoke("Remove", 2f);
    }

    private void Ignored()
    {
        canInteract = false;
        GetComponentInChildren<TextMeshPro>().text = "...";
        soundFx.PlayOneShot(SoundManager.instance.sounds[3]);

        float random = Random.Range(0f, 100f);

        int[] amount = { 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 25, 25, 25, 25, 25, 50, 100 };
        int index = Random.Range(0, amount.Length);

        if (random <= 75f)
        {
            FeralityManager.instance.removeFerality(amount[index]);
        }
        else
        {
            ScoreManager.instance.RemoveResource(amount[index]);
        }

        Invoke("Remove", 2f);

        Invoke("Remove", 2f);
    }

    private void Remove()
    {
        Instantiate(EffectsManager.instance.particles[2], transform.position, EffectsManager.instance.particles[2].transform.rotation);

        gameObject.SetActive(false);
    }
}
