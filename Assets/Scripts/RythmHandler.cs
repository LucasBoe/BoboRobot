using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmHandler : MonoBehaviour
{
    public static RythmHandler instance;

    bool active = true;

    public delegate void OnTick();
    public static OnTick Tick;

    public delegate void OnTickBegin();
    public static OnTickBegin TickBegin;

    public delegate void OnTickReceive();
    public static OnTickReceive TickReceive;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        StartCoroutine(ItickRoutine());
    }

    IEnumerator ItickRoutine() {
        while (active) {

            TickBegin?.Invoke();
            TickReceive?.Invoke();
            Tick?.Invoke();

            yield return new WaitForSeconds(1);
        }
    }
}
