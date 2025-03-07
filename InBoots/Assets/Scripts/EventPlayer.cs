using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventPlayer : MonoBehaviour
{
    protected virtual void Init() { }

    private void Awake()
    {
        Init();
    }
}
