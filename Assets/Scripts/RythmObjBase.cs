﻿using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmObjBase : MonoBehaviour
{
    [ShowNonSerializedField] bool last;
    [SerializeField] protected bool current;

    [SerializeField] RythmObjBase before;

    SpriteAnimator spriteAnimator;
    [SerializeField] SpriteAnimation onAnim, offAnim;

    [Button]
    protected void TryConnect() {

        RythmObjBase candidate = null;
        float candidateDistance = float.MaxValue;

        Object[] rythmObjs = Object.FindObjectsOfType<RythmObjBase>();

        foreach(Object o in rythmObjs) {
            RythmObjBase robj = (RythmObjBase)o;

            if (robj != null && robj != this) {

                if (candidate == null || candidateDistance > Vector2.Distance(transform.position,robj.transform.position)) {
                
                    candidate = robj;
                    candidateDistance = Vector2.Distance(transform.position,robj.transform.position);
                }

            }
        }

        if (candidate != null)
        {
            before = candidate;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        RythmHandler.TickBegin += OnTickBegin;

        RythmHandler.Tick += OnTick;

        RythmHandler.TickReceive += OnTickReceive;

        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    public bool ReceiveState() {
        return last;
    }

    protected virtual void OnTickBegin() {
        last = current;
    }

    protected virtual void OnTickReceive()
    {

        if (before != null)
            current = before.ReceiveState();
    }

    protected virtual void OnTick()
    {
        spriteAnimator?.Play(current ? onAnim : offAnim);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        if (current)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.5f, 0.2f);

        if (before != null)
            Gizmos.DrawLine(transform.position,before.transform.position);
    }
}
