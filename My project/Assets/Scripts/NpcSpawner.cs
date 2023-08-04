using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private float xBound;
    [SerializeField] private float yBound;
    [SerializeField] private float spawnTimerMin, spawnTimerMax;
    private float currentSpawnTimer;

    [SerializeField] private GameObject npc;

    private void Start()
    {
        currentSpawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
    }

    private void Update()
    {
        currentSpawnTimer -= Time.deltaTime;

        if (currentSpawnTimer <= 0f)
        {
            Vector3 spawnPos = new Vector3(Random.Range(xBound, -xBound), Random.Range(yBound, -yBound));

            Instantiate(npc, spawnPos + transform.position, npc.transform.rotation);

            currentSpawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(xBound * 2, yBound * 2));
    }
}
