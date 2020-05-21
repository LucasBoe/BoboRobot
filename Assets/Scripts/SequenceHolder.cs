using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceHolder : RythmObjBase
{
    [SerializeField] List<bool> stateInput;
    [SerializeField] GameObject sequenceEditorPrefab;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                Edit(this);
            }
        }
    }

    public List<bool> GetSequence() {
        return new List<bool>(states);
    }

    public void SetSequence(List<bool> _sequence) {
        states = new Queue<bool>(_sequence);
    }

    private void Edit(SequenceHolder sequenceHolder)
    {
        SequenceEditor sequenceEditor = Instantiate(sequenceEditorPrefab, transform.position, Quaternion.identity).GetComponent<SequenceEditor>();
        sequenceEditor.Init(sequenceHolder);
    }
}
