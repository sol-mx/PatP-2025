using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static DirectionalMinigameEntry;

public class DirectionalMinigamePlayer : MinigamePlayer
{
    [Header("Monitor")]
    private GameManager.EventEntry currentEntry;
    private DirectionalMinigameEntry currentSubEntry;
    [SerializeField] private DirectionalMinigameProfile currentProfile;

    public enum Input { UP, DOWN, LEFT, RIGHT }
    private readonly Dictionary<int, Input> inputByIndex = new() { { 0, Input.UP }, { 1, Input.DOWN }, { 2, Input.LEFT }, { 3, Input.RIGHT } };

    [Space(10), SerializeField] private bool pollInput;
    [SerializeField] private List<Input> neededInput;
    [SerializeField] private List<Input> currentInput;
    [SerializeField] private int currentErrors;

    public void StartMinigame(GameManager.EventEntry entry, DirectionalMinigameProfile profile)
    {
        currentEntry = entry;
        currentProfile = profile;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        foreach (var entry in currentProfile.Entries)
        {
            currentSubEntry = entry;

            neededInput.Clear();
            currentInput.Clear();
            currentErrors = 0;
            textDisplay.text = "";

            foreach (var word in entry.Words)
            {
                neededInput.Add(inputByIndex[Random.Range(0, 4)]);
                textDisplay.text += word.Question + " ";

                yield return new WaitForSeconds(delayPerWord);
            }

            yield return new WaitForSeconds(questionDuration);

            pollInput = true;
            textDisplay.text = "";

            var elapsed = 0f;

            while (pollInput)
            {
                elapsed += Time.deltaTime;

                if (elapsed >= responseDuration)
                {
                    GameOver();
                    yield break;
                }

                yield return null;
            }
        }

        yield return null;
        currentEntry.End();
    }

    private void GameOver()
    {
        pollInput = false;
        Debug.Log("Game Over");
    }

    // =================================================================================================================

    public void InputUp(InputAction.CallbackContext _)
    {
        InputKey(Input.UP);
    }

    public void InputDown(InputAction.CallbackContext _)
    {
        InputKey(Input.DOWN);
    }

    public void InputLeft(InputAction.CallbackContext _)
    {
        InputKey(Input.LEFT);
    }

    public void InputRight(InputAction.CallbackContext _)
    {
        InputKey(Input.RIGHT);
    }

    public void InputKey(Input key)
    {
        if (!pollInput) return;
        currentInput.Add(key);

        textDisplay.text += currentSubEntry.Words[currentInput.Count - 2].Reply + " ";

        if (neededInput[currentInput.Count - 1] != key) currentErrors++;
        if (currentErrors > allowedErrors) GameOver();
    }

    // =================================================================================================================

    [Header("Settings")]
    [SerializeField] private float questionDuration = 3.5f;
    [SerializeField] private float delayPerWord = 0.35f;
    [SerializeField] private float responseDuration = 3.5f;
    [SerializeField] private int allowedErrors;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textDisplay;
}
