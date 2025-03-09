using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReactionaryMinigameProfile", menuName = "EventProfile/Minigame/Duel", order = 5)]
public class ReactionaryMinigameProfile : MinigameProfile
{
    [field: Header("Settings")]
    [SerializeField] private float minCooldownUntilInputState;
    [SerializeField] private float maxCooldownUntilInputState;
    public float DelayUntilInputState => Random.Range(minCooldownUntilInputState, maxCooldownUntilInputState);

    [field: Space(10), SerializeField] public float InputWindow { get; private set; }

    [field: Header("Enable Gimmicks")]
    [field: SerializeField] public bool EnableRequireAim { get; private set; } // player must input a direction between up, middle, down
}
