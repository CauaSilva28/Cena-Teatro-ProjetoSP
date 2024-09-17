using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LerLivro : MonoBehaviour
{
    public GameObject cameraEscritorio;
    public GameObject Player;
    public GameObject PlayerEscritorio;

    public Text frasesTeclas;

    public GameObject telaLivro;

    public Transform posicaoLendo;

    public GameObject ElementosFalas;
    public Text falas;

    public Animator transicaoFalas;
    public Animator transicaoLari;

    public GameObject telaTransicao;

    public Pausar pauseJogo;

    private bool lerLivro = false;
    private bool livroAberto = false;
    private bool impedirVariasFalas = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lerLivro && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(telaIngresso());
            lerLivro = false;
        }

        if (livroAberto && !impedirVariasFalas && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(fimDaFase());
            impedirVariasFalas = true;
        }
    }
    
    IEnumerator fimDaFase(){
        ElementosFalas.SetActive(true);
        telaLivro.SetActive(false);
        pauseJogo.perdendo = true;

        yield return new WaitForSeconds(1f);

        falas.enabled = true;
        falas.text = "Seja lá quem estiver espalhando esses mucos, foi para o teatro municipal.";
        transicaoFalas.SetInteger("transition", 1);
        transicaoLari.SetInteger("transition", 1);

        yield return new WaitForSeconds(2f); // Tempo para exibir a fala

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // Espera até que o jogador pressione a tecla Espaço

        transicaoFalas.SetInteger("transition", 2);
        transicaoLari.SetInteger("transition", 2);

        yield return new WaitForSeconds(1f); // Tempo para transição entre as falas

        ElementosFalas.SetActive(false);

        yield return new WaitForSeconds(1f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(2f);

        AudioListener.volume = 0;

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Fase2");
    }

    IEnumerator telaIngresso(){
        Player.SetActive(false);
        PlayerEscritorio.SetActive(true);
        cameraEscritorio.SetActive(true);
        telaLivro.SetActive(true);
        frasesTeclas.enabled = false;
        frasesTeclas.text = "";

        yield return new WaitForSeconds(1f);

        livroAberto = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            frasesTeclas.enabled = true;
            frasesTeclas.text = "Aperte \"F\" para ler";
            lerLivro = true;
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            frasesTeclas.enabled = false;
            frasesTeclas.text = "";
            lerLivro = false;
        }
    }
}
