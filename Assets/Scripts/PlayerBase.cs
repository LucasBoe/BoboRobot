using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBase : MonoBehaviour
{
    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D collider;
    protected PlayerAnimationHandler animator;
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody == null)
            Debug.LogError("No Rigidbody2D found, please add one.");

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rigidbody == null)
            Debug.LogError("No SpriteRenderer found, please add one.");

        collider = GetComponent<BoxCollider2D>();

        if (rigidbody == null)
            Debug.LogError("No BoxCollider2D found, please add one.");

        animator = GetComponent<PlayerAnimationHandler>();

        if (animator == null)
            Debug.LogError("No PlayerAnimationHandler found, please add one.");
    }
}
