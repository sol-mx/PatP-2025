using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashMinigamePlayer : MinigamePlayer
{
    private GameManager.EventEntry currentEntry;
    [SerializeField] private MashMinigameProfile currentProfile;

    public void StartMinigame(GameManager.EventEntry entry, MashMinigameProfile profile)
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
