using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameChoices {
    NONE,
    ROCK,
    PAPER,
    SCISSORS
 }

public class JokenpoControle : MonoBehaviour
{
    public int pontosPlayer = 0;
    public int pontosBot = 0;
    public GameObject canva;
    public GameObject txtVitoria;
    public GameObject txtDerrota;
    public bool acabouJogo = false;
    public Inimigo inimigo;
    public GameObject areaInvisivel;
    public GameObject brunaAtivar;

    [SerializeField]
    private Sprite rock_sprite, paper_sprite, scissors_sprite;

    [SerializeField]
    private Image playerChoice_Img, opChoice_img;

    [SerializeField]
    private Text infoText;

    private GameChoices player_Choice = GameChoices.NONE, op_Choice = GameChoices.NONE;
    private ControleAnimacao animacaoControle; 

void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cmcMover"))
        {
            inimigo.podeMover = true;  
        }
    }

    void Update(){
         if (pontosPlayer == 3 && !acabouJogo){
            canva.gameObject.SetActive(false);
            txtVitoria.gameObject.SetActive(true);
            acabouJogo = true;
            Invoke("desativartxt",2);
            areaInvisivel.SetActive(true);
            brunaAtivar.SetActive(true);
            pontosBot=0;
            pontosPlayer=0;
        }
        if (pontosBot == 3 && !acabouJogo){
            canva.gameObject.SetActive(false);
            txtDerrota.gameObject.SetActive(true);
            Invoke("desativartxt",2);
            pontosBot=0;
            pontosPlayer=0;
        }
    }
    
    void Awake(){
        animacaoControle = GetComponent<ControleAnimacao>();
     }
     public void SetChoices(GameChoices gameChoices) {

        switch(gameChoices){
            case GameChoices.ROCK:
            player_Choice = GameChoices.ROCK;
            playerChoice_Img.sprite = rock_sprite;
            break;

            case GameChoices.PAPER:
            player_Choice = GameChoices.PAPER;
            playerChoice_Img.sprite = paper_sprite;
            break;

            case GameChoices.SCISSORS:
            player_Choice = GameChoices.SCISSORS;
            playerChoice_Img.sprite = scissors_sprite;
            break;
        }
        SetOpChoice();
        
        DetermineWinner();

    }

    void SetOpChoice(){
        int rnd = Random.Range(0, 3);

        switch(rnd){
            case 0:
            op_Choice = GameChoices.ROCK;
            opChoice_img.sprite = rock_sprite;
            break;

            case 1:
            op_Choice = GameChoices.PAPER;
            opChoice_img.sprite = paper_sprite;
            break;

            case 2:
            op_Choice = GameChoices.SCISSORS;
            opChoice_img.sprite = scissors_sprite;
            break;
        }
    }
    void DetermineWinner(){
        if(player_Choice == op_Choice){
        
        infoText.text = "Empate";
        StartCoroutine(DisplayWinnerAndRestart());
        return;
        }
        if(player_Choice == GameChoices.PAPER && op_Choice == GameChoices.ROCK){
         infoText.text = "Ganhou!";
        StartCoroutine(DisplayWinnerAndRestart());
        pontosPlayer ++;
        return;
        }
        if(op_Choice == GameChoices.PAPER && player_Choice == GameChoices.ROCK){
        infoText.text = "Perdeu!";
        StartCoroutine(DisplayWinnerAndRestart());
        pontosBot ++;
        return;
        }
        if(player_Choice == GameChoices.ROCK && op_Choice == GameChoices.SCISSORS){
        infoText.text = "Ganhou!";
        StartCoroutine(DisplayWinnerAndRestart());
        pontosPlayer ++;
        return;
        }
        if(op_Choice == GameChoices.ROCK && player_Choice == GameChoices.SCISSORS){
        infoText.text = "Perdeu!";
        StartCoroutine(DisplayWinnerAndRestart());
        pontosBot ++;
        return;
        }
        if(player_Choice == GameChoices.SCISSORS && op_Choice == GameChoices.PAPER){
        infoText.text = "Ganhou!";
        StartCoroutine(DisplayWinnerAndRestart());
        pontosPlayer ++;
        return;
        }
        if(op_Choice == GameChoices.SCISSORS && player_Choice == GameChoices.PAPER){
        infoText.text = "Perdeu!";
        StartCoroutine(DisplayWinnerAndRestart());
        pontosBot ++;
        return;
        }
       
    }
    public void desativartxt(){
        txtVitoria.gameObject.SetActive(false);
        txtDerrota.gameObject.SetActive(false);
    }
    IEnumerator DisplayWinnerAndRestart(){
        yield return new WaitForSeconds(1f);
        infoText.gameObject.SetActive(true);
    
        yield return new WaitForSeconds(1f);
        infoText.gameObject.SetActive(false);

        animacaoControle.ResetAnimation();    
    }

}