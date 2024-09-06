using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerLivro : MonoBehaviour
{
    public GameObject cameraPerso;
    public GameObject cameraEscritório;
    public GameObject Player;

    public Text frasesTeclas;

    public GameObject textoLivro;

    private bool lerLivro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lerLivro)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                cameraPerso.SetActive(false);
                cameraEscritório.SetActive(true);
                Player.GetComponent<MovimentoPerso>().enabled = false;
                textoLivro.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            frasesTeclas.enabled = true;
            frasesTeclas.text = "Aperte \"F\" para ler o livro";
            lerLivro = true;
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            frasesTeclas.enabled = false;
            frasesTeclas.text = "";
            lerLivro = false;
        }
    }
}
