using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputControle : MonoBehaviour
{
    private ControleAnimacao animacaoControle;
    private JokenpoControle controleJokenpo;

    private string playerChoice;

    void Awake() {
        animacaoControle = GetComponent<ControleAnimacao>();
        controleJokenpo = GetComponent<JokenpoControle>();
    }

    public void GetChoice(){
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        GameChoices selectedChoice = GameChoices.NONE;

        switch(choiceName){
            case "Rock":
            selectedChoice = GameChoices.ROCK;
            break;

            case "Paper":
            selectedChoice = GameChoices.PAPER;
            break;

            case "Scissors":
            selectedChoice = GameChoices.SCISSORS;
            break;
        }
       controleJokenpo.SetChoices(selectedChoice);
       animacaoControle.PlayerMadechoise();
    }
}
