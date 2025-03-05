using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : MonoBehaviour
{
    [field: SerializeField] public bool Ended { get; protected set; }

    private void Awake()
    {
        Init();
    }

    public virtual void Init() { }
    public abstract void StartRoutine();
    public abstract void EndRoutine();
}
