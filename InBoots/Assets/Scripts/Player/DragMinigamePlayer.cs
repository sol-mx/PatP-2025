using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMinigamePlayer : MinigamePlayer
{
    private GameManager.EventEntry currentEntry;
    [SerializeField] private DragMinigameProfile currentProfile;

    public void StartMinigame(GameManager.EventEntry entry, DragMinigameProfile profile)
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
