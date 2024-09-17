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
    public GameObject brunaAtivar;
    public GameObject barreiraInicio;
    public GameObject Player;
    public GameObject elementosJokenpo;
    public Animator animJade;
    public Animator animLari;

    [SerializeField]
    private Sprite rock_sprite, paper_sprite, scissors_sprite;

    [SerializeField]
    private Image playerChoice_Img, opChoice_img;

    [SerializeField]
    private Text infoText;

    private GameChoices player_Choice = GameChoices.NONE, op_Choice = GameChoices.NONE;
    private ControleAnimacao animacaoControle; 

    void Update(){
         if (pontosPlayer == 3 && !acabouJogo){
            StartCoroutine(Ganhou());
            animJade.SetBool("derrota", true);
            animJade.SetBool("jokenpo", false);
            pontosPlayer=0;
            pontosBot=0;
        }
        if (pontosBot == 3 && !acabouJogo){
            StartCoroutine(Perdeu());
            pontosPlayer=0;
            pontosBot=0;
        }
    }

    IEnumerator Perdeu(){

        yield return new WaitForSeconds(2f);

        animacaoControle.ResetAnimation();
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(1f);

        txtDerrota.SetActive(true);
        canva.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        txtDerrota.SetActive(false);
        Player.SetActive(true);
        elementosJokenpo.SetActive(false);
    }

    IEnumerator Ganhou(){
        yield return new WaitForSeconds(2f);

        txtVitoria.SetActive(true);
        canva.gameObject.SetActive(false);   
        Cursor.lockState = CursorLockMode.Locked; 

        yield return new WaitForSeconds(2f);

        Player.SetActive(true);
        elementosJokenpo.SetActive(false);
        acabouJogo = true;
        txtVitoria.SetActive(false);
        brunaAtivar.SetActive(true);
        barreiraInicio.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
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

    IEnumerator DisplayWinnerAndRestart(){
        animJade.SetBool("jokenpo", true);
        animLari.SetBool("jokenpo", true);
        yield return new WaitForSeconds(1f);
        infoText.gameObject.SetActive(true);
    
        yield return new WaitForSeconds(2f);
        infoText.gameObject.SetActive(false);

        animacaoControle.ResetAnimation();
        animJade.SetBool("jokenpo", false);
        animLari.SetBool("jokenpo", false);
    }

}