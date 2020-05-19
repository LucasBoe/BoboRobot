using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCable : RythmObjBase
{
    [SerializeField] SpriteRenderer highlight;

    protected override void OnTick()
    {
        highlight.color = current ? Color.white : Color.black;
    }
}
