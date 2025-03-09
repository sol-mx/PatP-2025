using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MashMinigamePlayer : MinigamePlayer
{
    [Header("Monitor")]
    [SerializeField] private MashMinigameProfile currentProfile;

    [Space(10), SerializeField] private float currentMeter;
    [SerializeField] private State currentState;

    [Space(10), SerializeField] private float cooldownUntilNextMashTiming;
    [SerializeField] private float mashTimingDuration;
    [Space(5), SerializeField] private float cooldownUntilNextPenaltyTiming;
    [SerializeField] private float penaltyTimingDuration;

    public enum State { STANDBY, MASH, PENALTY_ON_MASH }

    [Header("Settings")]
    [SerializeField] private float meterPerInput;
    [SerializeField] private float decrementPerWronglyTimedInput;
    [SerializeField] private float decrementPerPenaltyTimingInput;

    private bool RoutineValid => currentMeter > 0 && currentMeter < currentProfile.ClearThreshold;
    
    public void StartMinigame(GameManager.EventEntry entry, MashMinigameProfile profile)
    {
        currentEntry = entry;
        currentProfile = profile;

        routine = StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        pollInput = true;
        ResetFields();

        while (RoutineValid)
        {
            currentMeter -= currentProfile.StaticDecrement * Time.deltaTime;

            if (currentState == State.STANDBY || currentState == State.MASH)
            {
                cooldownUntilNextMashTiming -= Time.deltaTime;

                if (cooldownUntilNextMashTiming <= 0)
                {
                    currentState = State.MASH;
                    commentManager.SetMashMinigameComment(currentState);

                    cooldownUntilNextMashTiming = currentProfile.MashTimingCooldown;
                    mashTimingDuration = currentProfile.MashTimingDuration;
                }

                if (mashTimingDuration > 0)
                {
                    mashTimingDuration -= Time.deltaTime;

                    if (mashTimingDuration <= 0)
                    {
                        currentState = State.STANDBY;
                        commentManager.SetMashMinigameComment(currentState);
                    }
                }
            }

            if (currentProfile.EnablePenaltyTiming && (currentState == State.STANDBY || currentState == State.PENALTY_ON_MASH))
            {
                cooldownUntilNextPenaltyTiming -= Time.deltaTime;

                if (cooldownUntilNextPenaltyTiming <= 0)
                {
                    currentState = State.PENALTY_ON_MASH;
                    commentManager.SetMashMinigameComment(currentState);

                    cooldownUntilNextPenaltyTiming = currentProfile.PenaltyTimingCooldown;
                    penaltyTimingDuration = currentProfile.PenaltyTimingDuration;
                }

                if (penaltyTimingDuration > 0)
                {
                    penaltyTimingDuration -= Time.deltaTime;

                    if (penaltyTimingDuration <= 0)
                    {
                        currentState = State.STANDBY;
                        commentManager.SetMashMinigameComment(currentState);
                    }
                }
            }

            yield return null;
        }

        if (currentMeter <= 0)
        {
            GameOver();
        }
        else if (currentMeter >= currentProfile.ClearThreshold)
        {
            Clear();
        }
    }

    private void ResetFields() 
    {
        currentMeter = currentProfile.ClearThreshold * 0.5f;

        currentState = State.STANDBY;
        commentManager.SetMashMinigameComment(currentState);

        RandomizeCooldowns();
    }

    private void RandomizeCooldowns()
    {
        cooldownUntilNextMashTiming = currentProfile.MashTimingCooldown;
        if (currentProfile.EnablePenaltyTiming) cooldownUntilNextPenaltyTiming = currentProfile.PenaltyTimingCooldown;
    }

    public void Mash(InputAction.CallbackContext _)
    {
        if (!pollInput) return;

        if (currentState == State.STANDBY) currentMeter -= decrementPerWronglyTimedInput;
        else if (currentState == State.MASH) currentMeter += meterPerInput;
        else if (currentState == State.PENALTY_ON_MASH) currentMeter -= decrementPerWronglyTimedInput;
    }

    // =================================================================================================================

    private void Update() // TEMP
    {
        if (!pollInput) return;

        debugText.text = $"{(int)currentMeter} / {(int)currentProfile.ClearThreshold}";
    }
}
