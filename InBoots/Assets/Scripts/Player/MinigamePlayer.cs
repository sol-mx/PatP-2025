using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MinigamePlayer : EventPlayer
{
    protected bool pollInput;
    protected GameManager.EventEntry currentEntry;
    protected Coroutine routine;

    protected CommentManager commentManager;
    protected TextMeshProUGUI debugText;

    private void Awake()
    {
        commentManager = FindObjectOfType<CommentManager>();
        debugText = GameObject.Find("Debug Text").GetComponent<TextMeshProUGUI>();
    }

    private void ReInit()
    {
        pollInput = false;

        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        commentManager.EmptyComment();
        debugText.text = "";
    }

    protected virtual void Clear()
    {
        ReInit();
        currentEntry?.End();
        Debug.Log("Clear!");
    }

    protected virtual void GameOver()
    {
        ReInit();
        Debug.Log("Game Over");
    }
}
