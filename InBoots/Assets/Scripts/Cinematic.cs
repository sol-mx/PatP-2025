using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cinematic : Event
{
    private PlayerController controller;

    public override void Init()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    public override void StartRoutine()
    {
        controller.DisableAll();
        StartCinematic();
    }

    public abstract void StartCinematic();
}
