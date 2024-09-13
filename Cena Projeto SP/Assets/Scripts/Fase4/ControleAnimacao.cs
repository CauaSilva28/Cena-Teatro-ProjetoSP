using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleAnimacao : MonoBehaviour
{
    [SerializeField]
    private Animator PlayerChoiceHandlerAnimation, choiceAnimation;
    
    public void ResetAnimation()
    {
        PlayerChoiceHandlerAnimation.Play("showHandler");
        choiceAnimation.Play("removeChoices");
    }
    public void PlayerMadechoise()
    {
        PlayerChoiceHandlerAnimation.Play("removeHandler");
        choiceAnimation.Play("showChoices");
    }
}
