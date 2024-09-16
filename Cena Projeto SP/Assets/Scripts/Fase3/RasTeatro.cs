using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RasTeatro : MonoBehaviour
{
    public bool SapoCriminoso;
    private bool verificarRa = false;

    public Text teclaCapturar;

    public GameObject ElementosFalas;
    public GameObject Saida;
    public Text falas;

    public Text fraseTelaSaia;

    public Animator transicaoFalas;
    public Animator transicaoLari;
    public Animator transicaoRa;

    public MovimentoPerso movimentoPerso;

    private float impedirVariasFalar = 0f;

    private string[] dialogo = {
        "Te encontrei! é você a culpada pelo assasinato no calçadão de Osasco.",
        "Eu? assasinato? como assim?",
        "Não se faça de desentendida!",
        "Você não pode me acusar desse jeito sem provas.",
        "Você está deixando mucos verdes por toda parte.",
        "Esses mucos verdes podem ser de qualquer um, se ta tão desconfiada, vamos até o Museu do Ipiranga para eu te mostrar algo.",
        "Por que eu iria?",
        "Acredito que lá você verá o que está procurando."
    };

    private Animator[] animarElementos;
    // Start is called before the first frame update
    void Start()
    {
        animarElementos = new Animator[] { transicaoLari, transicaoRa, transicaoLari, transicaoRa, transicaoLari, transicaoRa, transicaoLari, transicaoRa };
    }

    // Update is called once per frame
    void Update()
    {
        if(verificarRa){
            ElementosFalas.SetActive(true);
            teclaCapturar.text = "";
            falas.enabled = true;
            movimentoPerso.emAreaDeFala = true;

            if(SapoCriminoso){
                if(impedirVariasFalar == 0f){
                    StartCoroutine(ExibirDialogoCriminosos());
                    impedirVariasFalar = 1f;
                }
            }
            else{
                if(impedirVariasFalar == 0f){
                    StartCoroutine(ExibirDialogoNaoCriminosos());
                    impedirVariasFalar = 1f;
                }
            }
        }
    }

    IEnumerator ExibirDialogoCriminosos(){
        for (int i = 0; i < dialogo.Length; i++)
        {
            falas.text = dialogo[i];
            transicaoFalas.SetInteger("transition", 1);
            animarElementos[i].SetInteger("transition", 1);

            yield return new WaitForSeconds(2f); // Tempo para exibir a fala

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // Espera até que o jogador pressione a tecla Espaço

            transicaoFalas.SetInteger("transition", 2);
            animarElementos[i].SetInteger("transition", 2);

            yield return new WaitForSeconds(1f); // Tempo para transição entre as falas
        }

        fraseTelaSaia.enabled = true;
        fraseTelaSaia.text = "Saia do teatro com a rã";
        ElementosFalas.SetActive(false);
        impedirVariasFalar = 0f;
        gameObject.SetActive(false);
        Saida.SetActive(true);
        verificarRa = false;
        movimentoPerso.emAreaDeFala = false;
    }

    IEnumerator ExibirDialogoNaoCriminosos(){
        falas.text = "Essa não é a criminosa...";
        transicaoFalas.SetInteger("transition", 1);
        transicaoLari.SetInteger("transition", 1);

        yield return new WaitForSeconds(2f); // Tempo para exibir a fala

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // Espera até que o jogador pressione a tecla Espaço

        transicaoFalas.SetInteger("transition", 2);
        transicaoLari.SetInteger("transition", 2);

        yield return new WaitForSeconds(1f); // Tempo para transição entre as falas

        ElementosFalas.SetActive(false);
        impedirVariasFalar = 0f;
        gameObject.SetActive(false);
        verificarRa = false;
        movimentoPerso.emAreaDeFala = false;
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Player")){
            teclaCapturar.enabled = true;
            teclaCapturar.text = "Aperte \"E\" para capturar a rã";
            if(Input.GetKey(KeyCode.E)){
                verificarRa = true;
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            teclaCapturar.enabled = false;
            teclaCapturar.text = "";
        }
    }
}
