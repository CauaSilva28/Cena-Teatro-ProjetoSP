using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicaoTela : MonoBehaviour
{
    public GameObject tela;
    public Animator telaTransicao;

    private float imperdirRepeticao = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(imperdirRepeticao == 0f){
            StartCoroutine(tempoTransicaoDaTela());
            imperdirRepeticao = 1f;
        }
    }

    IEnumerator tempoTransicaoDaTela(){
        telaTransicao.SetInteger("transition", 1);

        yield return new WaitForSeconds(2f);

        tela.SetActive(false);
    }
}
