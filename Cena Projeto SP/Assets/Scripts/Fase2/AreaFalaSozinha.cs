using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaFalaSozinha : MonoBehaviour
{
    public float tempoExibirFala;

    public GameObject ElementosFalas;
    public GameObject textoPassarFalas;
    public Text falas;

    public string textoFalas;

    public Animator transicaoFalas;
    public Animator transicaoLari;

    private bool playerNaArea = false;
    private bool impedirVariasFalas = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNaArea && !impedirVariasFalas)
        {
            StartCoroutine(fala());
            impedirVariasFalas = true;
        }
    }

    IEnumerator fala(){
        textoPassarFalas.SetActive(false);
        ElementosFalas.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        falas.enabled = true;
        falas.text = textoFalas;
        transicaoFalas.SetInteger("transition", 1);
        transicaoLari.SetInteger("transition", 1);

        yield return new WaitForSeconds(tempoExibirFala); // Tempo para exibir a fala

        transicaoFalas.SetInteger("transition", 2);
        transicaoLari.SetInteger("transition", 2);

        yield return new WaitForSeconds(1f); // Tempo para transição entre as falas

        ElementosFalas.SetActive(false);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerNaArea = true;
        }
    }
}
