using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionaryMinigamePlayer : MinigamePlayer
{
    private GameManager.EventEntry currentEntry;
    [SerializeField] private ReactionaryMinigameProfile currentProfile;

    public void StartMinigame(GameManager.EventEntry entry, ReactionaryMinigameProfile profile)
    {
        currentEntry = entry;
        currentProfile = profile;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return null;
        currentEntry.End();
    }
}
