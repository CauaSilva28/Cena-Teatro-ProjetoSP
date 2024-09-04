using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falas : MonoBehaviour
{   
    public GameObject Sair;
    public GameObject Prox;
    public GameObject msgClick;
    public GameObject[] mensagem; 
    public int mensagemAtual = 0;
    public GameObject falas;
   
    public void NextMessage()
    {
        if (mensagemAtual < mensagem.Length - 1)
        {
            mensagem[mensagemAtual].SetActive(false);

            mensagemAtual++;

            mensagem[mensagemAtual].SetActive(true);
        }
    }
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "crime")
            {
                msgClick.SetActive(true);
            }
             else
            {
                msgClick.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.tag == "crime" )
        {
            falas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true; 
        }
        }
        if (mensagemAtual == 2);
        {
        Sair.SetActive(true);
        }   
    }
   
    public void FecharFala()
    {
        falas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

}
