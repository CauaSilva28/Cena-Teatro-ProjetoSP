using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaidaTeatro : MonoBehaviour
{
    public GameObject Tela;
    public Animator telaTransicao;

    public Text fraseTecla;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Player")){
            fraseTecla.enabled = true;
            fraseTecla.text = "Aperte \"E\" para sair do teatro";

            if(Input.GetKey(KeyCode.E)){
                Tela.SetActive(true);
                telaTransicao.SetInteger("transition", 2);
            }
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            fraseTecla.enabled = false;
            fraseTecla.text = "";
        }
    }
}
