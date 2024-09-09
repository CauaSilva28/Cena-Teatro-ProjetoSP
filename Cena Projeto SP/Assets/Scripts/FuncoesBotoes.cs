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

    private int numFase;

    public void IniciarJogo(){
        numFase = 1;
        StartCoroutine(iniciarFase());
        somClick.Play();
    }
    public void IniciarFase2(){
        numFase = 2;
        StartCoroutine(iniciarFase());
        somClick.Play();
    }
    public void IniciarFase3(){
        numFase = 3;
        StartCoroutine(iniciarFase());
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

    public void SairParaMenu(){
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void SairJogo(){
        Application.Quit();
        somClick.Play();
    }

    public void Reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    IEnumerator iniciarFase(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Fase" + numFase);
    }
}
