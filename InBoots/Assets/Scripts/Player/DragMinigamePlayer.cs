using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMinigamePlayer : MinigamePlayer
{
    [SerializeField] private DragMinigameProfile currentProfile;

    public void StartMinigame(GameManager.EventEntry entry, DragMinigameProfile profile)
    {
        currentEntry = entry;
        currentProfile = profile;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        // logic goes here...

        // END MINIGAME
        yield return null;
        currentEntry.End();
    }
}
