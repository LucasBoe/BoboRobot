using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomperBehaviour : RythmObjBase
{
    [SerializeField] BoxCollider2D trigger;

    protected override void Start()
    {
        base.Start();

        trigger = GetComponent<BoxCollider2D>();
        trigger.enabled = false;
    }

    protected override void OnTick()
    {
        base.OnTick();

        if (current)
            StartCoroutine(IStomperTriggerRoutine());
    }

    IEnumerator IStomperTriggerRoutine() {
        yield return new WaitForSeconds(0.33f);
        trigger.enabled = true;
        yield return new WaitForSeconds(0.33f);
        trigger.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBase.Kill();
        }
    }
}
