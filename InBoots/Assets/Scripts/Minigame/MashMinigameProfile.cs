using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MashMinigameProfile", menuName = "EventProfile/Minigame/Debate", order = 4)]
public class MashMinigameProfile : MinigameProfile
{
    [field: Header("Settings")]
    [field: SerializeField, Tooltip("")] public float ClearThreshold { get; private set; }
    [field: SerializeField, Tooltip("Per second.")] public float StaticDecrement { get; private set; }

    [Space(10), SerializeField] private float mashTimingMinDuration;
    [SerializeField] private float mashTimingMaxDuration;
    public float MashTimingDuration => Random.Range(mashTimingMinDuration, mashTimingMaxDuration);

    [Space(10), SerializeField] private float mashTimingMinCooldown;
    [SerializeField] private float mashTimingMaxCooldown;
    public float MashTimingCooldown => Random.Range(mashTimingMinCooldown, mashTimingMaxCooldown);

    [field: Header("Enable Gimmicks")]
    [field: SerializeField] public bool EnablePenaltyTiming { get; private set; } // opponent often triggers a length of time where input is even more penalized
    [field: SerializeField] public bool EnableOnlyUseSpecifcKeys { get; private set; } // player is penalized when inputting via keys not specified

    [field: Header("Advanced Settings")]
    [SerializeField] private float penaltyTimingMinDuration;
    [SerializeField] private float penaltyTimingMaxDuration;
    public float PenaltyTimingDuration => Random.Range(penaltyTimingMinDuration, penaltyTimingMaxDuration);

    [Space(10), SerializeField] private float penaltyTimingMinCooldown;
    [SerializeField] private float penaltyTimingMaxCooldown;
    public float PenaltyTimingCooldown => Random.Range(penaltyTimingMinCooldown, penaltyTimingMaxCooldown);
}
