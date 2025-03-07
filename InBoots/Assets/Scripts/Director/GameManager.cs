using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool suspend;

    [Space(10), SerializeField] private EventSequence sequenceOriginal;
    [SerializeField] private List<EventEntry> sequence = new();

    private EventManager eventManager;
    private PlayerController controller;

    private IntroDirector intro;
    private OutroDirector outro;

    [Serializable]
    public class EventEntry
    {
        [field: SerializeField] public EventProfile Profile { get; private set; }
        [field: SerializeField] public bool Ended { get; private set; }

        public EventEntry(EventProfile profile)
        {
            Profile = profile;
            Ended = false;
        }

        public void End()
        {
            Ended = true;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (var e in sequenceOriginal.Val)
        {
            sequence.Add(new EventEntry(e));
        }

        eventManager = FindObjectOfType<EventManager>();
        controller = FindObjectOfType<PlayerController>();

        intro = FindObjectOfType<IntroDirector>();
        outro = FindObjectOfType<OutroDirector>();
    }

    private void Start()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        if (suspend)
        {
            yield break;
        }

        intro.StartRoutine();

        yield return new WaitUntil(() => IntroDirector.Ended);

        foreach (var entry in sequence)
        {
            Debug.Log($"Starting event: {entry.Profile.name}");

            eventManager.StartEvent(entry);
            controller.EnableScheme(entry.Profile);

            yield return new WaitUntil(() => entry.Ended);

            controller.DisableScheme(entry.Profile);

            Debug.Log($"Event ended: {entry.Profile.name}");
        }

        outro.StartRoutine();
    }
}
