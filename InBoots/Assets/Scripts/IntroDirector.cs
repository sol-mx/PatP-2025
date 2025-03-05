using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDirector : MonoBehaviour
{
    public static bool Ended { get; private set; }
    [SerializeField] private bool skip;

    public void StartRoutine()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        if (skip)
        {
            Ended = true;
            yield break;
        }

        yield return null;
        Ended = true;
    }
}
