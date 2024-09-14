using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisoesFase3 : MonoBehaviour
{
    public Text gosma;
    public Text frasesTela;

    public AudioSource somTrancada;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Porta")){
            somTrancada.Play();
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("GosmaRa")){
            gosma.enabled = true;
        }
        if(other.gameObject.CompareTag("Porta")){
            frasesTela.text = "Essa sala está trancada";
            frasesTela.enabled = true;
        }
    }
    
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("GosmaRa")){
            gosma.enabled = false;
        }
        if(other.gameObject.CompareTag("Porta")){
            frasesTela.text = "";
            frasesTela.enabled = false;
        }
    }
}
