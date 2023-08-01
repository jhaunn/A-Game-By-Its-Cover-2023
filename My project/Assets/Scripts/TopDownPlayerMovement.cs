using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0f || movement.x < 0f)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", 0f);
        }

        if (movement.y > 0f || movement.y < 0f)
        {
            animator.SetFloat("LastVertical", movement.y);
            animator.SetFloat("LastHorizontal", 0f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement.normalized);
    }
}
