using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionalMinigamePlayer : MinigamePlayer
{
    [Header("Monitor")]
    [SerializeField] private DirectionalMinigameProfile currentProfile;
    private DirectionalMinigameQuestion currentQuestion;

    public enum Input { UP, DOWN, LEFT, RIGHT }
    private readonly Dictionary<int, Input> inputByIndex = new() { { 0, Input.UP }, { 1, Input.DOWN }, { 2, Input.LEFT }, { 3, Input.RIGHT } };
    private readonly Dictionary<Input, string> labelByInput = new() { { Input.UP, "up" }, { Input.DOWN, "down" }, { Input.LEFT, "left" }, { Input.RIGHT, "right" } };

    [Space(10), SerializeField] private List<Input> neededInput;
    [SerializeField] private List<Input> currentInput;
    [SerializeField] private int currentErrors;

    public void StartMinigame(GameManager.EventEntry entry, DirectionalMinigameProfile profile)
    {
        currentEntry = entry;
        currentProfile = profile;

        routine = StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        foreach (var question in currentProfile.Questions)
        {
            currentQuestion = question;

            neededInput.Clear();
            currentInput.Clear();
            currentErrors = 0;
            debugText.text = "";

            foreach (var word in question.Words)
            {
                var randomInput = inputByIndex[Random.Range(0, 4)];
                neededInput.Add(randomInput);

                debugText.text += word.Question + " ";
                debugText.text += $"({labelByInput[randomInput]})" + " "; // TEMP

                yield return new WaitForSeconds(delayPerWord);
            }

            yield return new WaitForSeconds(questionDuration);

            commentManager.SetDirectionalMinigameComment(neededInput);
            
            pollInput = true;
            debugText.text = "";

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

            commentManager.SetComment("Great!");
            yield return new WaitForSeconds(delayPerQuestion);
            commentManager.EmptyComment();
        }

        Clear();
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

        debugText.text += currentQuestion.Words[currentInput.Count - 1].Reply + " ";

        if (neededInput[currentInput.Count - 1] != key)
        {
            currentErrors++;
            Debug.Log("Incorrect input!");
        }
        else
        {
            Debug.Log("Correct input!");

            if (neededInput.Count == currentInput.Count)
            {
                Debug.Log("Clear!");

                pollInput = false;
                return;
            }
        }

        if (currentErrors > allowedErrors) GameOver();
    }

    // =================================================================================================================

    [Header("Settings")]
    [SerializeField] private float questionDuration = 3.5f;
    [SerializeField] private float responseDuration = 3.5f;
    [Space(10), SerializeField] private float delayPerWord = 0.35f;
    [SerializeField] private float delayPerQuestion = 0.5f;
    [Space(10), SerializeField] private int allowedErrors;
}
