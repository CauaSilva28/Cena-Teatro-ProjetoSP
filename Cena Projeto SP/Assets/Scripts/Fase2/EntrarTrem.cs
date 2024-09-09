using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntrarTrem : MonoBehaviour
{
    private float veloTrem = 0f;

    public GameObject player;
    public GameObject cameraTrem;
    public GameObject trem;
    public GameObject telaTransicao;

    public AudioSource buzinaTrem;
    public AudioSource tremAndando;

    public Text frasesTecla;

    private bool chegouNoTrem = false;
    private bool tremMovimentar = false;

    public Pausar pauseJogo;
    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("TremBuzinar", 5, 10); 
    }

    // Update is called once per frame
    void Update()
    {
        if(tremMovimentar){
            MovimentoAutomaticoTrem();
        }

        if(chegouNoTrem){
            if(Input.GetKey(KeyCode.F)){
                StartCoroutine(fimFase());
                chegouNoTrem = false;
            }
        }
    }

    void TremBuzinar(){
        buzinaTrem.Play();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            chegouNoTrem = true;
            frasesTecla.enabled = true;
            frasesTecla.text = "Aperte \"F\" para entrar no trem";
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            chegouNoTrem = false;
            frasesTecla.enabled = false;
            frasesTecla.text = "";
        }
    }

    void MovimentoAutomaticoTrem(){
        veloTrem = 40f * Time.deltaTime; 
        Vector3 tremAndando = new Vector3(veloTrem, 0, 0);
        trem.GetComponent<Transform>().transform.Translate(tremAndando);
    }
    
    IEnumerator fimFase(){
        pauseJogo.perdendo = true;
        frasesTecla.enabled = false;
        frasesTecla.text = "";
        cameraTrem.SetActive(true);
        player.SetActive(false);
        tremAndando.Play();

        yield return new WaitForSeconds(2f);

        tremMovimentar = true;
        
        yield return new WaitForSeconds(4f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(3f);

        cameraTrem.GetComponent<AudioListener>().enabled = false;

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Fase3");
    }
}
