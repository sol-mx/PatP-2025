using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroDirector : MonoBehaviour
{
    private Coroutine routine;

    public void StartRoutine()
    {
        if (routine != null) return;
        routine = StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        Debug.Log("Outro statrted");

        yield return null;
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
