using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceHolder : RythmObjBase
{
    [SerializeField] List<bool> stateInput;
    Queue<bool> states;

    protected override void Start()
    {
        base.Start();
        states = new Queue<bool>(stateInput);
    }

    protected override void OnTickReceive()
    {
        if (states.Count > 0)
        {
            current = states.Dequeue();
            states.Enqueue(current);
        }
    }
}
