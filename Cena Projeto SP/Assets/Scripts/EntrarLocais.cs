using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrarLocais : MonoBehaviour
{
    public Transform posicaoTeleporte;
    public Text textoTeclas;

    public string textoNasTeclas;

    public GameObject player;

    public GameObject telaTransicaoTeleporte;

    private bool teleportar = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportar)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(EntrarLocal());
                teleportar = false;
            }
        }
    }

    IEnumerator EntrarLocal()
    {
        telaTransicaoTeleporte.SetActive(true);
        telaTransicaoTeleporte.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(1f);

        player.GetComponent<MovimentoPerso>().enabled = false;
        Vector3 posicaoPlayer = posicaoTeleporte.position;
        player.GetComponent<Transform>().position = posicaoPlayer;

        yield return new WaitForSeconds(2f);

        telaTransicaoTeleporte.GetComponent<Animator>().SetInteger("transition", 2);
        player.GetComponent<MovimentoPerso>().enabled = true;

        yield return new WaitForSeconds(2f);

        telaTransicaoTeleporte.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoTeclas.enabled = true;
            textoTeclas.text = textoNasTeclas;
            teleportar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoTeclas.enabled = false;
            textoTeclas.text = "";
            teleportar = false;
        }
    }
}
