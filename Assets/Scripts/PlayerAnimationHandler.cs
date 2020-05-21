using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] SpriteAnimation parkAnim, driveAnim, turnAnim, duckDriveAnim, duckParkAnim, smashed;

    [ShowNonSerializedField] PlayerState currentState = PlayerState.PARK;
    [ShowNonSerializedField] PlayerState overlayState = PlayerState.NONE;

    SpriteAnimator spriteAnimator;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimator = GetComponent<SpriteAnimator>();

        if (spriteAnimator == null)
            Debug.LogError("No SpriteAnimator found, please add one.");
    }

    private void SetState(PlayerState _state, bool asOverlay = false)
    {
        if (asOverlay)
        {
            overlayState = _state;
            spriteAnimator.Play(StateToAnimation(overlayState));
            StopAllCoroutines();
            StartCoroutine(IOverlayRoutine());
            
        } else
        {
            currentState = _state;

            if (overlayState == PlayerState.NONE)
                spriteAnimator.Play(StateToAnimation(currentState));
        }
    }

    public void TryChangeState(PlayerState _state, bool asOverlay = false)
    {
        if (GetState(asOverlay) != _state)
            SetState(_state, asOverlay);
    }

    public PlayerState GetState(bool getOverlay = false)
    {
        if (getOverlay)
            return overlayState;
        else
            return currentState;
    }

    IEnumerator IOverlayRoutine()
    {
        while (overlayState != PlayerState.NONE && !spriteAnimator.IsDone())
        {
            yield return null;
        }

        overlayState = PlayerState.NONE;
        spriteAnimator.Play((StateToAnimation(currentState)));
    }

    private SpriteAnimation StateToAnimation(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.DRIVE: return driveAnim;
            case PlayerState.TURN: return turnAnim;
            case PlayerState.DUCKDRIVE: return duckDriveAnim;
            case PlayerState.DUCKPARK: return duckParkAnim;
            case PlayerState.SMASHED: return smashed;
        }

        return parkAnim;
    }
}

public enum PlayerState
{
    NONE,
    PARK,
    DRIVE,
    TURN,
    DUCKDRIVE,
    DUCKPARK,
    SMASHED
}
