using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerMovement : PlayerBase
{
    [ShowNonSerializedField] float moveVelocity = 10;

    [SerializeField] SpriteAnimation driveAnim, turnAnim;
    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        float yVelocityBefore = 0;

        if (rigidbody.velocity.y < 0 && GetOrientation() == PlayerOrientation.UP)
            yVelocityBefore = rigidbody.velocity.y;

        rigidbody.velocity = transform.right * xInput * moveVelocity + yVelocityBefore * Vector3.up;

        

        if (xInput < 0)
            spriteRenderer.flipX = true;

        if (xInput > 0)
            spriteRenderer.flipX = false;

        if (xInput == 0)
        {
            rigidbody.velocity = Vector3.zero;
            return;
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

    protected override void SetOrientation(PlayerOrientation _orientation = PlayerOrientation.UP)
    {
        base.SetOrientation(_orientation);
        rigidbody.position = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), Mathf.Floor(transform.position.z)) + Vector3.one * 0.5f;
        animator.Play(turnAnim,true,driveAnim);
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
}
