using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colisoes : MonoBehaviour

{
    public GameObject musica;
    public GameObject Bloqueio;
    public float contaMuco = 0;
    public Text txtmucopego;
    public GameObject mucopego;

    public GameObject telaGameOver;
    public GameObject Audios;

    public GameObject AudioPessoas;

    public GameObject telaTransicaoTeleporte;

    public Transform entradaEscritorio;

    public Pausar pauseJogo;

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
            txtmucopego.text = "Mucos pegos: " + contaMuco + "/10";
        }

        if (other.gameObject.tag == "musicas")
        {
            musica.SetActive(true);
        }

        if (other.gameObject.tag == "Parte2")
        {
            StartCoroutine(teleportarEscritorio());
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
        pauseJogo.perdendo = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        telaGameOver.SetActive(true);
        Audios.SetActive(false);
        GetComponent<MovimentoPerso>().emAreaDeFala = true;
    }

    IEnumerator teleportarEscritorio()
    {
        telaTransicaoTeleporte.SetActive(true);
        telaTransicaoTeleporte.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(1f);

        Vector3 posicaoPlayerEscritorio = entradaEscritorio.position;
        transform.position = posicaoPlayerEscritorio;
        GetComponent<MovimentoPerso>().enabled = false;
        AudioPessoas.SetActive(false);

        yield return new WaitForSeconds(1f);

        telaTransicaoTeleporte.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(0.5f);

        GetComponent<MovimentoPerso>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        telaTransicaoTeleporte.GetComponent<Animator>().SetInteger("transition", 0);
        telaTransicaoTeleporte.SetActive(false);
    }

}

