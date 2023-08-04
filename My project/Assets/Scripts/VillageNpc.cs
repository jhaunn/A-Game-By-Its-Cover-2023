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
            canInteract = false;
            GetComponentInChildren<TextMeshPro>().text = "...";
            soundFx.PlayOneShot(SoundManager.instance.sounds[3]);
            Invoke("Remove", 2f);
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
                canInteract = false;
                soundFx.PlayOneShot(SoundManager.instance.sounds[4]);
                Invoke("Remove", 2f);
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

    private void Remove()
    {
        Instantiate(EffectsManager.instance.particles[2], transform.position, EffectsManager.instance.particles[2].transform.rotation);
        gameObject.SetActive(false);
    }
}
