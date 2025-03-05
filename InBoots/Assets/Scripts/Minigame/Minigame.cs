using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : Event
{
    private PlayerController controller;
    [SerializeField] private PlayerController.ControlScheme controlScheme;

    public override void Init()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    public override void StartRoutine()
    {
        controller.EnableScheme(controlScheme);
        StartMinigame();
    }

    public override void EndRoutine()
    {
        controller.DisableScheme(controlScheme);
        Ended = true;
    }

    public abstract void StartMinigame();
}
