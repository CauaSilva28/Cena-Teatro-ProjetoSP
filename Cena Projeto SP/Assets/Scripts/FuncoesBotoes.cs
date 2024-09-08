using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncoesBotoes : MonoBehaviour
{
    public GameObject telaTransicao;
    public AudioSource somClick;

    public GameObject telaMenu;
    public GameObject telaFases;

    public void IniciarJogo(){
        StartCoroutine(iniciarJogo());
        somClick.Play();
    }

    public void AbrirTelaFases(){
        telaMenu.SetActive(false);
        telaFases.SetActive(true);
        somClick.Play();
    }

    public void VoltarMenu(){
        telaMenu.SetActive(true);
        telaFases.SetActive(false);
        somClick.Play();
    }

    public void SairJogo(){
        Application.Quit();
        somClick.Play();
    }

    public void Reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator iniciarJogo(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Fase1");
    }
}
