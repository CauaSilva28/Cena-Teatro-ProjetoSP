using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoAtendente : MonoBehaviour
{
    public GameObject ElementosFalas;
    public GameObject Barragem;

    public MovimentoPerso movimentoPerso;

    public Text teclaInicioDialogo;
    public Text falas;

    public Animator transicaoFalas;
    public Animator transicaoAten;
    public Animator transicaoLari;

    private float tempoIniciarFala = 0f;

    private bool inicioDialogo = false;

    private string[] dialogo = {
        "Boa tarde! seja bem vinda ao teatro municipal de São Paulo...",
        "Boa tarde! Me chamo Larissa e estou em busca de uma rã criminosa, viu alguma por aqui?",
        "Hoje vi diversas rãs diferentes entrando no teatro, uma em específico está soltando gosmas verdes por aí.",
        "Essa mesmo que estou atrás! Irei procurar pelo teatro.",
        "Ok, boa sorte!"
    };

    private Animator[] animarElementos;

    void Start()
    {
        animarElementos = new Animator[] { transicaoAten, transicaoLari, transicaoAten, transicaoLari, transicaoAten };
    }

    void Update()
    {
        if (inicioDialogo)
        {
            Destroy(Barragem);
            teclaInicioDialogo.enabled = false;
            teclaInicioDialogo.text = "";
            ElementosFalas.SetActive(true);
            falas.enabled = true;
            movimentoPerso.emAreaDeFala = true;

            if (tempoIniciarFala == 0f)
            {
                StartCoroutine(ExibirDialogo());
                tempoIniciarFala = 1f; // Impedir que a corrotina seja chamada várias vezes
            }
        }
    }

    IEnumerator ExibirDialogo()
    {
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

        ElementosFalas.SetActive(false);
        gameObject.SetActive(false);
        inicioDialogo = false;
        tempoIniciarFala = 0f;
        movimentoPerso.emAreaDeFala = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teclaInicioDialogo.enabled = true;
            teclaInicioDialogo.text = "Aperte \"E\" para falar com o atendente";
            if (Input.GetKey(KeyCode.E))
            {
                inicioDialogo = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teclaInicioDialogo.enabled = false;
            teclaInicioDialogo.text = "";
        }
    }
}
