using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReactionaryMinigamePlayer : MinigamePlayer
{
    [Header("Monitor")]
    [SerializeField] private ReactionaryMinigameProfile currentProfile;

    [Space(10), SerializeField] private float currentDelayUntilInputState;
    [SerializeField] private Aim neededAim;

    [Space(10), SerializeField] private State currentState;
    [SerializeField] private Aim currentAim;

    public enum State { STANDBY, FIRE }
    public enum Aim { UP, MIDDLE, DOWN }

    public void StartMinigame(GameManager.EventEntry entry, ReactionaryMinigameProfile profile)
    {
        currentEntry = entry;
        currentProfile = profile;

        routine = StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        pollInput = true;
        ResetFields();

        commentManager.SetComment("Standby...");

        currentDelayUntilInputState = currentProfile.DelayUntilInputState;
        if (currentProfile.EnableRequireAim) neededAim = (Aim)Random.Range(0, 3);

        while (currentDelayUntilInputState > 0)
        {
            currentDelayUntilInputState -= Time.deltaTime;
            yield return null;
        }

        //

        currentState = State.FIRE;

        if (currentProfile.EnableRequireAim) commentManager.SetReactionaryMinigameComment(neededAim);
        else commentManager.SetReactionaryMinigameComment();

        var remainingInputWindow = currentProfile.InputWindow;

        while (remainingInputWindow > 0)
        {
            remainingInputWindow -= Time.deltaTime;
            yield return null;
        }

        GameOver();
        if (pollAim != null) StopCoroutine(pollAim);
    }

    private void ResetFields()
    {
        currentState = State.STANDBY;

        currentAim = Aim.MIDDLE;
        debugText.text = $"Aiming {currentAim}";
    }

    public void Fire(InputAction.CallbackContext _)
    {
        if (currentState == State.STANDBY)
        {
            GameOver();
            if (pollAim != null) StopCoroutine(pollAim);
        }
        else
        {
            if (currentState == State.FIRE)
            {
                if (currentProfile.EnableRequireAim)
                {
                    if (currentAim != neededAim)
                    {
                        GameOver();
                        if (pollAim != null) StopCoroutine(pollAim);

                        return;
                    }
                }

                Clear();
                if (pollAim != null) StopCoroutine(pollAim);
            }
        }
    }

    private Coroutine pollAim;

    public void AimUp(InputAction.CallbackContext context)
    {
        if (pollAim != null) StopCoroutine(pollAim);
        pollAim = StartCoroutine(PollAim(context, Aim.UP));
    }

    public void AimDown(InputAction.CallbackContext context)
    {
        if (pollAim != null) StopCoroutine(pollAim);
        pollAim = StartCoroutine(PollAim(context, Aim.DOWN));
    }

    private IEnumerator PollAim(InputAction.CallbackContext context, Aim aim)
    {
        currentAim = aim;
        debugText.text = $"Aiming {currentAim}";

        while (context.performed)
        {
            yield return null;
        }

        currentAim = Aim.MIDDLE;
        debugText.text = $"Aiming {currentAim}";

        pollAim = null;
    }
}
