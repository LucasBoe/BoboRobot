using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOrientation
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public static class PlayerOrientationExtention
{
    public static Vector3 ToVector (this PlayerOrientation orientation)
    {
        switch (orientation)
        {
            case PlayerOrientation.UP:
                return Vector3.up;
            case PlayerOrientation.RIGHT:
                return Vector3.right;
            case PlayerOrientation.DOWN:
                return Vector3.down;
            case PlayerOrientation.LEFT:
                return Vector3.left;
        }

        return Vector3.zero;
    }

    public static PlayerOrientation ToOpposite(this PlayerOrientation orientation)
    {
        return (PlayerOrientation)((int)(orientation + 2) % 4);
    }
}
public class PlayerBase : MonoBehaviour
{
    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D collider;
    protected SpriteAnimator animator;

    private PlayerOrientation orientation;
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

        animator = GetComponent<SpriteAnimator>();

        if (animator == null)
            Debug.LogError("No SpriteAnimator found, please add one.");
    }

    protected virtual void SetOrientation(PlayerOrientation _orientation = PlayerOrientation.UP)
    {
        orientation = _orientation;

        Debug.Log("orientation set to " + orientation);

        float _z = 0;

        switch (orientation)
        {
            case PlayerOrientation.LEFT:
                _z = 90;
                break;

            case PlayerOrientation.RIGHT:
                _z = -90;
                break;

            case PlayerOrientation.DOWN:
                _z = 180;
                break;
        }

        transform.rotation = Quaternion.Euler(0,0,_z);
    }

    public PlayerOrientation GetOrientation()
    {
        return orientation;
    }
}
