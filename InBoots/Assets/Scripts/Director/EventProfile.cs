using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventProfile : ScriptableObject
{
    [field: SerializeField, TextArea(3, 5)] public string Description { get; private set; }

    public enum Type { CINEMATIC, MINIGAME }
    [field: Space(10), SerializeField] public Type EventType { get; protected set; }
}
