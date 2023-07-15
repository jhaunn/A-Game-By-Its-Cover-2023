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
    private bool isPlanted = false;

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, interactRadius, playerLayerMask))
        {
            text.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !isPlanted)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = plant.growth[0];
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
