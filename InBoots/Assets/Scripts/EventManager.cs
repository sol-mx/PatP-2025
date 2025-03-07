using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private CinematicDirector cinematicDirector;
    private DirectionalMinigamePlayer directionalMinigameManager;
    private DragMinigamePlayer dragMinigameManager;
    private MashMinigamePlayer mashMinigameManager;
    private ReactionaryMinigamePlayer reactionaryMinigameManager;

    private void Awake()
    {
        cinematicDirector = FindObjectOfType<CinematicDirector>();
        directionalMinigameManager = FindObjectOfType<DirectionalMinigamePlayer>();
        dragMinigameManager = FindObjectOfType<DragMinigamePlayer>();
        mashMinigameManager = FindObjectOfType<MashMinigamePlayer>();
        reactionaryMinigameManager = FindObjectOfType<ReactionaryMinigamePlayer>();
    }

    public void StartEvent(GameManager.EventEntry entry)
    {
        if (entry.Profile is CinematicProfile cinematicProfile)
        {
            cinematicDirector.StartCinematic(entry, cinematicProfile);
        }
        else if (entry.Profile is MinigameProfile minigameProfile)
        {
            if (minigameProfile is DirectionalMinigameProfile dirProfile)
            {
                directionalMinigameManager.StartMinigame(entry, dirProfile);
            }
            else if (minigameProfile is DragMinigameProfile dragProfile)
            {
                dragMinigameManager.StartMinigame(entry, dragProfile);
            }
            else if (minigameProfile is MashMinigameProfile mashProfile)
            {
                mashMinigameManager.StartMinigame(entry, mashProfile);
            }
            else if (minigameProfile is ReactionaryMinigameProfile reactionaryProfile)
            {
                reactionaryMinigameManager.StartMinigame(entry, reactionaryProfile);
            }
        }
    }
}
