using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaInicial : MonoBehaviour
{
    public GameObject telaTransicao;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mudarCena());
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator mudarCena(){
        yield return new WaitForSeconds(10f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Fase1");
    }
}
