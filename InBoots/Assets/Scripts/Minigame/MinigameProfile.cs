using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameProfile : EventProfile
{
    public new enum Type { DINNER, GAME, DEBATE, DUEL }
    [field: SerializeField] public Type MinigameType { get; protected set; }
}
