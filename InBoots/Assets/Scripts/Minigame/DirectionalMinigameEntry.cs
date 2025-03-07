using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectionalMinigameEntry", menuName = "EventEntry/Directional", order = 7)]
public class DirectionalMinigameEntry : ScriptableObject
{
    [SerializeField] private string question;
    [SerializeField] private string reply;

    [field: Space(10), SerializeField] public List<Word> Words { get; private set; }

    [Serializable]
    public class Word
    {
        public enum Type { NORMAL, FAKE, DOUBLE }
        [field: SerializeField] public Type InputType { get; private set; }

        [field: Space(10), SerializeField] public string Question { get; private set; }
        [field: SerializeField] public string Reply { get; private set; }

        public void SetText(string question, string reply)
        {
            Question = question;
            Reply = reply;
        }
    }

    [ContextMenu("Auto-Fill Questions and Replies")]
    private void AutoFillQuestionsAndReplies()
    {
        var wordsInQuestion = question.Split(' ');
        var wordsInReply = reply.Split(' ');

        for (int i = 0; i < Words.Count; i++)
        {
            if (wordsInQuestion.Length - 1 < i || wordsInReply.Length - 1 < i) return;
            Words[i].SetText(wordsInQuestion[i], wordsInReply[i]);
        }
    }
}
