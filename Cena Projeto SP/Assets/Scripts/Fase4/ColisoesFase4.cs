using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColisoesFase4 : MonoBehaviour
{
    public Transform porta;
    public AudioSource portaFechando;
    public AudioSource coletandoItem;
    public Transform portaCorredor;

    public int itemCount = 0;
    public int totalItems = 3;

    public GameObject espadaEstatua;
    public Inimigo inimigo;
    public GameObject canvaJokenpo;
    public GameObject txtTeclaE;
    public Text textoTela;
    public GameObject areaFalaComJade;
    public GameObject areaBrunaDesativar;
    public GameObject brunaConversaJade;
    public GameObject telaGameOver;
    public GameObject Audios;
    public GameObject elementosJokenpo;
    public Pausar pauseJogo;
    public JokenpoControle jokenpoScript;

    private bool naAreaJokenpo = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("caixa"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            telaGameOver.SetActive(true);
            pauseJogo.perdendo = true;
            Audios.SetActive(false);
            GetComponent<MovimentoPerso>().emAreaDeFala = true;
        }

        if (other.gameObject.CompareTag("EnigmaMuseu"))
        {
            porta.eulerAngles = new Vector3(-90, 0, -90);
            portaFechando.Play();
            brunaConversaJade.SetActive(true);
            Destroy(other.gameObject);
            textoTela.enabled = true;
            textoTela.text = "Procure as 3 espadas perdidas da estátua";
        }
        if (other.gameObject.CompareTag("desativarObjeto"))
        {
            brunaConversaJade.SetActive(false);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "item")
        {
            coletandoItem.Play();
            Destroy(other.gameObject);
            itemCount++;

            if (itemCount >= totalItems)
            {
                AtivaEspada();
                textoTela.enabled = false;
                textoTela.text = "";
                areaFalaComJade.SetActive(true);
                areaBrunaDesativar.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("cmcMover"))
        {
            inimigo.podeMover = true;

        }
        if (other.gameObject.CompareTag("Jokenpo"))
        {
            if(!jokenpoScript.acabouJogo){
                txtTeclaE.SetActive(true);
                naAreaJokenpo = true;
            }
        }

    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jokenpo"))
        {
            txtTeclaE.SetActive(false);
            naAreaJokenpo = false;
        }
    }

    void Update()
    {
        if(!jokenpoScript.acabouJogo){
            if (naAreaJokenpo && Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
                elementosJokenpo.SetActive(true);
                canvaJokenpo.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }

            if(canvaJokenpo.activeSelf == true){
                txtTeclaE.SetActive(false);
                naAreaJokenpo = false;
            }
        }
    }
    void Start()
    {
        if (espadaEstatua != null)
        {
            espadaEstatua.SetActive(false);
        }
    }

    void AtivaEspada()
    {
        if (espadaEstatua != null)
        {
            espadaEstatua.SetActive(true);
            porta.eulerAngles = new Vector3(-90, 0, 0);
            portaCorredor.eulerAngles = new Vector3(-90, 0, 0);

        }
    }
}
