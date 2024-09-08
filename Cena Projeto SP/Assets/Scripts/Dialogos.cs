using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogos : MonoBehaviour
{
    public GameObject ElementosFalas;
    public GameObject Barragem;

    public MovimentoPerso movimentoPerso;

    public Text teclaInicioDialogo;
    public Text falas;

    public string textoTeclaApertar;

    public Animator transicaoFalas;

    private float tempoIniciarFala = 0f;

    private bool inicioDialogo = false;

    public string[] dialogo;

    public Animator[] animarElementos;

    void Start()
    {
        
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
            teclaInicioDialogo.text = textoTeclaApertar;
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
