using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogosNPC : MonoBehaviour
{
    [Header("Objetos")]
    public MovimentoPerso movePerso;
    public GameObject telaTransicao;
    public Pausar pauseJogo;

    [Header("Diálogo")]
    public string[] dialogueNpc; 
    public GameObject txtParaInteragir;

    [Header("Interface de Diálogo")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text nameNpc;
    public Image imageNpc; 
    public int dialogueIndex = 0;

   [Header("Controle de Diálogo")]
    public bool readyToSpeak; 
    public bool startDialogue; 

    public float segundosletras;
    public string nameNpcString; 

    void Start() { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyToSpeak)
        {
            if (!startDialogue)
            {
                StartDialogue();
                movePerso.emAreaDeFala = true;
            }
            else if (dialogueText.text == dialogueNpc[dialogueIndex])
            {
                NextDialogue(); 
            }
        }
    }
    void NextDialogue()
    {
        dialogueIndex++; 
        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(ShowDialogue()); 
        }
        else
        {
            startDialogue = true;
            dialogueIndex = 0;
            dialoguePanel.SetActive(false);
            movePerso.emAreaDeFala = false;
            StartCoroutine(fimDaFase());
        }
    }

    public void StartDialogue()
    {
        txtParaInteragir.SetActive(false);

        nameNpc.text = nameNpcString; 
        startDialogue = true; 
        dialogueIndex = 0; 
        dialoguePanel.SetActive(true); 
        StartCoroutine(ShowDialogue()); 
    }

    IEnumerator ShowDialogue()
    {
        dialogueText.text = ""; 
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(segundosletras); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            readyToSpeak = true; 

            if (!startDialogue)
            {
                txtParaInteragir.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            readyToSpeak = false; 

            if (!startDialogue)
            {
                txtParaInteragir.SetActive(false);
            }
        }
    }

    IEnumerator fimDaFase(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);
        pauseJogo.perdendo = true;

        yield return new WaitForSeconds(3f);

        AudioListener.volume = 0;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Fase5");
    }
}

