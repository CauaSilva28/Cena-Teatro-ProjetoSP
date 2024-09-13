using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogosNPC : MonoBehaviour
{
    [Header("Diálogo")]
    public string[] dialogueNpc; 
    public GameObject txtEParaInteragir;

    [Header("Interface de Diálogo")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text nameNpc;
    public Image imageNpc; 
    public Sprite spriteNpc;
    public int dialogueIndex = 0;

   [Header("Controle de Diálogo")]
    public bool readyToSpeak; 
    public bool startDialogue; 

    public float segundosletras;
    public string nameNpcString; 

    void Start() { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && readyToSpeak)
        {
            if (!startDialogue)
            {
                StartDialogue(); 
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
        }
    }

    public void StartDialogue()
    {
        txtEParaInteragir.SetActive(false);

        nameNpc.text = nameNpcString; 
        imageNpc.sprite = spriteNpc; 
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
        if (other.CompareTag("Lari"))
        {
            readyToSpeak = true; 

            if (!startDialogue)
            {
                txtEParaInteragir.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lari"))
        {
            readyToSpeak = false; 

            if (!startDialogue)
            {
                txtEParaInteragir.SetActive(false);
            }
        }
    }
}

