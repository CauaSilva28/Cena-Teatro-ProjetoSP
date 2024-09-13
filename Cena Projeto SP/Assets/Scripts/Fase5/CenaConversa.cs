using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CenaConversa : MonoBehaviour
{
    public GameObject ElementosFalas;
    public GameObject textoPassarFalas;
    public Text falas;

    public string textoFalas;

    public Animator transicaoFalas;
    public Animator transicaoLari;

    public GameObject bruna1;
    public GameObject cameraConversa;
    public GameObject player;
    public GameObject playerConversa;
    public GameObject telaLivroJorge;

    public GameObject telaTransicao;
    public GameObject textoTempoPassado;

    public GameObject ElementosJogo;
    public GameObject cutsceneFim;
    
    public Dialogos dialogo;
    public Pausar pause;

    private bool fecharLivro = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogo.inicioDialogo){
            bruna1.SetActive(false);
            player.SetActive(false);
            playerConversa.SetActive(true);
            cameraConversa.SetActive(true);
        }

        if(dialogo.fimDialogo && !fecharLivro){
            telaLivroJorge.SetActive(true);

            if(Input.GetKey(KeyCode.F)){
                StartCoroutine(falaSozinha());
                telaLivroJorge.SetActive(false);
                cameraConversa.SetActive(false);
                playerConversa.SetActive(false);
                player.SetActive(true);
                fecharLivro = true;
            }
        }
    }

    IEnumerator falaSozinha(){
        textoPassarFalas.SetActive(false);
        ElementosFalas.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        falas.enabled = true;
        falas.text = textoFalas;
        transicaoFalas.SetInteger("transition", 1);
        transicaoLari.SetInteger("transition", 1);

        yield return new WaitForSeconds(10f); // Tempo para exibir a fala

        transicaoFalas.SetInteger("transition", 2);
        transicaoLari.SetInteger("transition", 2);

        yield return new WaitForSeconds(1f); // Tempo para transição entre as falas

        ElementosFalas.SetActive(false);
        pause.perdendo = true;

        yield return new WaitForSeconds(1f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        textoTempoPassado.SetActive(true);

        yield return new WaitForSeconds(4f);

        textoTempoPassado.SetActive(false);
        cutsceneFim.SetActive(true);
        ElementosJogo.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        
        cutsceneFim.GetComponent<PlayableDirector>().Play();
    }

    public void VoltarParaMenu(){
        SceneManager.LoadScene("Menu");
    }
}
