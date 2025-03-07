using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDirector : EventPlayer
{
    private GameManager.EventEntry currentEntry;
    [SerializeField] private CinematicProfile currentProfile;

    public void StartCinematic(GameManager.EventEntry entry, CinematicProfile profile)
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
