using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaidaTeatro : MonoBehaviour
{
    public GameObject Tela;

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
                StartCoroutine(fimDaFase());
            }
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            fraseTecla.enabled = false;
            fraseTecla.text = "";
        }
    }

    IEnumerator fimDaFase(){
        Tela.SetActive(true);
        Tela.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(3f);

        AudioListener.volume = 0;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Fase4");
    }
}
