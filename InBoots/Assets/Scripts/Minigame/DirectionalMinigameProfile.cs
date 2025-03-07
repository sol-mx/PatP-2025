using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectionalMinigameProfile", menuName = "EventProfile/Minigame/Dinner", order = 2)]
public class DirectionalMinigameProfile : MinigameProfile
{
    [field: SerializeField] public List<DirectionalMinigameEntry> Entries { get; private set; }
}
