using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private bool suspend;

    private IntroDirector intro;
    private OutroDirector outro;

    [SerializeField, Tooltip("Must be ordered as intended")] private List<Event> events;

    private void Awake()
    {
        intro = FindObjectOfType<IntroDirector>();
        outro = FindObjectOfType<OutroDirector>();
    }

    private IEnumerator Routine()
    {
        if (suspend)
        {
            yield break;
        }

        intro.StartRoutine();

        yield return new WaitUntil(() => IntroDirector.Ended);

        foreach (var e in events)
        {
            e.StartRoutine();
        }
    }
}
