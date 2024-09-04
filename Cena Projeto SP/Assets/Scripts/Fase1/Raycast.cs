using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject msgClick;
    public GameObject msgLivro;
    public GameObject painelSenha;
    public GameObject camera1;
    public GameObject camera2; 
    public GameObject anotacao;
    
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "senha")
            {
                msgClick.SetActive(true);
            }
            else
            {
                msgClick.SetActive(false);
            }
            if (hit.collider.gameObject.tag == "livro")
            {
                msgLivro.SetActive(true);
            }
            else
            {
                msgLivro.SetActive(false);
            }
            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.tag == "livro")
            {
                
                anotacao.SetActive(true);
                camera1.SetActive(false);
                camera2.SetActive(true);
                msgLivro.SetActive(false);
            }
            else {
                anotacao.SetActive(false);
                camera1.SetActive(true);
                camera2.SetActive(false);
            }
        }
        else
        {
            msgClick.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.tag == "senha" )
        {
            painelSenha.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;        
        }
        
    }
}
