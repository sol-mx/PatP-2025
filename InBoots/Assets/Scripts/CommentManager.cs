using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetComment(string text)
    {
        this.text.text = text;
    }

    public void EmptyComment()
    {
        text.text = "";
    }

    private void Awake()
    {
        EmptyComment();
    }

    // =================================================================================================================

    public static string GetDirectionalMinigameComment(List<DirectionalMinigamePlayer.Input> neededInputs)
    {
        var result = "Quick! Enter ";

        foreach (var input in neededInputs)
        {
            result += $"<color=red>{input.ToString().ToLower()}</color>";
            if (input != neededInputs[^1]) result += ", ";
        }

        return result;
    }

    public void SetDirectionalMinigameComment(List<DirectionalMinigamePlayer.Input> neededInputs)
    {
        SetComment(GetDirectionalMinigameComment(neededInputs));
    }

    // =================================================================================================================

    // =================================================================================================================

    public static string GetMashMinigameComment(MashMinigamePlayer.State currentTiming)
    {
        if (currentTiming == MashMinigamePlayer.State.STANDBY)
        {
            return "Hold your horses...";
        }
        else if (currentTiming == MashMinigamePlayer.State.MASH)
        {
            return "Mash!";
        }
        else if (currentTiming == MashMinigamePlayer.State.PENALTY_ON_MASH)
        {
            return "Stop! Definitely not now!";
        }
        else
        {
            return "";
        }
    }

    public void SetMashMinigameComment(MashMinigamePlayer.State currentTiming)
    {
        SetComment(GetMashMinigameComment(currentTiming));
    }

    // =================================================================================================================

    private static Dictionary<ReactionaryMinigamePlayer.Aim, string> labelByAim = new() 
    { { ReactionaryMinigamePlayer.Aim.UP, "upwards" }, { ReactionaryMinigamePlayer.Aim.MIDDLE, "in the middle"}, { ReactionaryMinigamePlayer.Aim.DOWN, "downwards" } };

    public static string GetReactionaryMinigameComment()
    {
        return "Shoot!";
    }

    public void SetReactionaryMinigameComment()
    {
        SetComment(GetReactionaryMinigameComment());
    }

    public static string GetReactionaryMinigameComment(ReactionaryMinigamePlayer.Aim neededAim)
    {
        return $"Shoot <color=red>{labelByAim[neededAim]}</color>!";
    }

    public void SetReactionaryMinigameComment(ReactionaryMinigamePlayer.Aim neededAim)
    {
        SetComment(GetReactionaryMinigameComment(neededAim));
    }
}
