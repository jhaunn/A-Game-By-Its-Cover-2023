using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private float interactRadius = 0.25f;
    [SerializeField] private LayerMask playerLayerMask;

    [SerializeField] private TextMeshPro text;

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, interactRadius, playerLayerMask))
        {
            text.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player pressed E at farm plot");
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
