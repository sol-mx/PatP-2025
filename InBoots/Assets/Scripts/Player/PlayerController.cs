
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Controls controls;

    private InputAction directional_up;
    private InputAction directional_down;
    private InputAction directional_left;
    private InputAction directional_right;

    private InputAction mash_fire;

    private InputAction reactionary_fire;
    private InputAction reactionary_up;
    private InputAction reactionary_down;

    private DirectionalMinigamePlayer directionalMinigame;
    private DragMinigamePlayer dragMinigame;
    private MashMinigamePlayer mashMinigame;
    private ReactionaryMinigamePlayer reactionaryMinigame;

    private void Awake()
    {
        controls = new();

        directional_up = controls.Directional.Up;
        directional_down = controls.Directional.Down;
        directional_left = controls.Directional.Left;
        directional_right = controls.Directional.Right;

        mash_fire = controls.Mash.Fire;

        reactionary_fire = controls.Reactionary.Fire;
        reactionary_up = controls.Reactionary.Up;
        reactionary_down = controls.Reactionary.Down;

        directionalMinigame = FindObjectOfType<DirectionalMinigamePlayer>();
        dragMinigame = FindObjectOfType<DragMinigamePlayer>();
        mashMinigame = FindObjectOfType<MashMinigamePlayer>();
        reactionaryMinigame = FindObjectOfType<ReactionaryMinigamePlayer>();
    }

    public void EnableScheme(EventProfile profile)
    {
        if (profile is CinematicProfile) DisableAll();
        else if (profile is DirectionalMinigameProfile) EnableDirectional();
        else if (profile is DragMinigameProfile) DisableAll();
        else if (profile is MashMinigameProfile) EnableMash();
        else if (profile is ReactionaryMinigameProfile) EnableReactionary();
    }

    public void DisableScheme(EventProfile profile)
    {
        if (profile is DirectionalMinigameProfile) DisableDirectional();
        else if (profile is MashMinigameProfile) DisableMash();
        else if (profile is ReactionaryMinigameProfile) DisableReactionary();
    }

    private void EnableDirectional()
    {
        directional_up.performed += directionalMinigame.InputUp;
        directional_down.performed += directionalMinigame.InputDown;
        directional_left.performed += directionalMinigame.InputLeft;
        directional_right.performed += directionalMinigame.InputRight;
    }

    private void DisableDirectional()
    {
        directional_up.performed -= directionalMinigame.InputUp;
        directional_down.performed -= directionalMinigame.InputDown;
        directional_left.performed -= directionalMinigame.InputLeft;
        directional_right.performed -= directionalMinigame.InputRight;
    }

    private void EnableMash()
    {
        mash_fire.performed += mashMinigame.Mash;
    }

    private void DisableMash()
    {
        mash_fire.performed -= mashMinigame.Mash;
    }

    private void EnableReactionary()
    {
        reactionary_fire.performed += reactionaryMinigame.Fire;
        reactionary_up.performed += reactionaryMinigame.AimUp;
        reactionary_down.performed += reactionaryMinigame.AimDown;
    }

    private void DisableReactionary()
    {
        reactionary_fire.performed -= reactionaryMinigame.Fire;
        reactionary_up.performed -= reactionaryMinigame.AimUp;
        reactionary_down.performed -= reactionaryMinigame.AimDown;
    }

    public void DisableAll()
    {
        DisableDirectional();
        DisableMash();
        DisableReactionary();
    }

    private void OnEnable()
    {
        controls?.Enable();
    }

    private void OnDisable()
    {
        controls?.Disable();
    }
}
