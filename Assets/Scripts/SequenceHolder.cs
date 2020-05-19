using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceHolder : RythmObjBase
{
    [SerializeField] List<bool> stateInput;
    Stack<bool> states;

    protected override void Start()
    {
        base.Start();
        states = new Stack<bool>(stateInput);
    }

    protected override void OnTickReceive()
    {
        if (states.Count > 0)
            current = states.Pop();
    }
}
