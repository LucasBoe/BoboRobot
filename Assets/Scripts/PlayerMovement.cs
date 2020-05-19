using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;


public enum PlayerOrientation
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public static class PlayerOrientationExtention
{
    public static Vector3 ToVector(this PlayerOrientation orientation)
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

public class PlayerMovement : PlayerBase
{
    [ShowNonSerializedField] float moveVelocity = 10;
    private PlayerOrientation orientation;

    protected override void Start()
    {
        base.Start();

        animator.TryChangeState(PlayerState.DRIVE);
    }
    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        rigidbody.velocity = transform.right * xInput * moveVelocity;

        bool duck = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (xInput < 0)
            spriteRenderer.flipX = true;

        if (xInput > 0)
            spriteRenderer.flipX = false;

        if (xInput == 0)
        {
            rigidbody.velocity = Vector3.zero;

            if (duck)
                animator.TryChangeState(PlayerState.DUCKPARK);
            else
                animator.TryChangeState(PlayerState.PARK);

            return;
        }
        else
        {
            if (duck)
                animator.TryChangeState(PlayerState.DUCKDRIVE);
            else
                animator.TryChangeState(PlayerState.DRIVE);
        }

        PlayerOrientation direction = InputToOrientation(xInput);

        Vector3 downleft = (GetOrientation().ToOpposite().ToVector() + direction.ToOpposite().ToVector()) / 2;

        bool infront = IsGroundInDirection(transform.position + direction.ToVector()/2, direction.ToVector(), 0.1f);
        bool below = IsGroundInDirection(transform.position + downleft,(GetOrientation().ToOpposite().ToVector()),0.1f);
        bool backleft = IsGroundInDirection(transform.position + downleft, downleft,0.5f);

        if (!backleft && !infront && !below)
            return;

        if (infront)
        {
            SetOrientation(direction.ToOpposite());
            rigidbody.velocity = InputToOrientation(xInput).ToVector() * rigidbody.velocity.magnitude;
        }
        else if (!below)
        {
            SetOrientation(direction);
            //rigidbody.velocity = Quaternion.Euler(0, 0, 90) * rigidbody.velocity;
            rigidbody.velocity = InputToOrientation(xInput).ToVector() * rigidbody.velocity.magnitude;
        }
    }

    protected void SetOrientation(PlayerOrientation _orientation = PlayerOrientation.UP)
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

        transform.rotation = Quaternion.Euler(0, 0, _z);

        rigidbody.position = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), Mathf.Floor(transform.position.z)) + Vector3.one * 0.5f;
        animator.TryChangeState(PlayerState.TURN, asOverlay: true);
    }

    PlayerOrientation InputToOrientation (float xInput)
    {
        int dir = xInput > 0 ? 1 : xInput < 0 ? -1 : 0;
        return (PlayerOrientation)((int)(GetOrientation() + dir + 4)% 4);
    }

    bool IsGroundInDirection(Vector2 position, Vector2 direction, float length)
    {
        LayerMask mask = LayerMask.GetMask("Walls");
        RaycastHit2D hit = Physics2D.Raycast(position, direction, length, mask);

        Debug.DrawLine(position, position + direction * length, Color.red, Time.deltaTime);

        // If it hits something...
        if (hit.point != null && hit.point != Vector2.zero)
        {
            Debug.Log("ground detected");
            return true;
        }

        return false;
    }

    public PlayerOrientation GetOrientation()
    {
        return orientation;
    }
}
