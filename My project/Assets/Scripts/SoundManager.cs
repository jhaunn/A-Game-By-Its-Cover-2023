using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] public AudioClip[] sounds;

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

    public AudioSource SetupAudio(GameObject source)
    {
        AudioSource soundFx = source.AddComponent<AudioSource>();

        soundFx.volume = 0.65f;
        soundFx.spatialBlend = 1f;
        soundFx.maxDistance = 10f;

        return soundFx;
    }
}
