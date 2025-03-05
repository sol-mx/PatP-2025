
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Controls controls;
    public enum ControlScheme {DIRECTIONAL, MASH, REACTIONARY};

    private InputAction directional_up;
    private InputAction directional_down;
    private InputAction directional_left;
    private InputAction directional_right;

    private InputAction mash_fire;

    private InputAction reactionary_fire;
    private InputAction reactionary_up;
    private InputAction reactionary_down;

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
    }

    public void EnableScheme(ControlScheme scheme)
    {
        if (scheme == ControlScheme.DIRECTIONAL) EnableDirectional();
        else if (scheme == ControlScheme.MASH) EnableMash();
        else if (scheme == ControlScheme.REACTIONARY) EnableReactionary();
    }

    public void DisableScheme(ControlScheme scheme)
    {
        if (scheme == ControlScheme.DIRECTIONAL) DisableDirectional();
        else if (scheme == ControlScheme.MASH) DisableMash();
        else if (scheme == ControlScheme.REACTIONARY) DisableReactionary();
    }

    private void EnableDirectional()
    {

    }

    private void DisableDirectional()
    {

    }

    private void EnableMash()
    {

    }

    private void DisableMash()
    {

    }

    private void EnableReactionary()
    {

    }

    private void DisableReactionary()
    {

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
