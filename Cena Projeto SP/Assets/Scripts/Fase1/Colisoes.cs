using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Colisoes : MonoBehaviour

{
    public GameObject musica;
    public GameObject Bloqueio;
    public float contaMuco = 0;
    public Text txtmucopego;
    public GameObject mucopego;

    public GameObject telaGameOver;
    public GameObject Audios;

    public string textoBlock;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Block"){
            if (contaMuco < 10)
            {
                GetComponent<MovimentoPerso>().frasesTela.enabled = true;
                GetComponent<MovimentoPerso>().frasesTela.text = textoBlock;
            }
        }

        if (collision.gameObject.tag == "Pessoa")
        {
            Derrota();
        }

        if (contaMuco >= 10)
        {
            Destroy(Bloqueio);
            mucopego.SetActive(false);
        }

        if (collision.gameObject.tag == "Parte2")
        {
            SceneManager.LoadScene("escritorio");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Block"){
            GetComponent<MovimentoPerso>().frasesTela.enabled = false;
            GetComponent<MovimentoPerso>().frasesTela.text = ""; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lari")
        {
            Destroy(other.gameObject);
            contaMuco++;
            txtmucopego.text = "Muco pego: " + contaMuco + "/10";
        }

        if (other.gameObject.tag == "musicas")
        {
            musica.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)    
    {
        if (other.gameObject.tag == "musicas")
        {
            musica.SetActive(false);
        }
    }

    private void Derrota(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        telaGameOver.SetActive(true);
        Audios.SetActive(false);
        GetComponent<MovimentoPerso>().enabled = true;
    }

}

