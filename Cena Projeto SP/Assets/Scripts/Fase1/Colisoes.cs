using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class pegarMeleca : MonoBehaviour

{
    public GameObject musica;
    public GameObject Bloqueio;
    public GameObject mensagem;
    public float contaMuco = 0;
    public TextMeshProUGUI  txtmucopego;
    public GameObject mucopego;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lari")
        {
            Destroy(collision.gameObject);
            contaMuco ++;
            txtmucopego.text="Muco pego: " + contaMuco;
        }
        if (collision.gameObject.tag == "Pessoa")
        {
            Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
            SceneManager.LoadScene("Perdeu");
        }
          if (collision.gameObject.tag == "Block")
        {
           mensagem.SetActive(true);
            Invoke("DestroyObject", 2f);
        }
        if (contaMuco == 10)
        {
            Destroy(Bloqueio);
            mucopego.SetActive(false);
        }
       if (collision.gameObject.tag == "Parte2")
        {
            SceneManager.LoadScene("escritorio");
        }
        
    }
    void DestroyObject()
    {
       mensagem.SetActive(false);
    }
    void OnTriggerStay(Collider batidao)    
{
        if (batidao.gameObject.tag == "som")
        {
            musica.SetActive(true);
        }
    }

  void OnTriggerExit(Collider batidao)    
  {
        musica.SetActive(false);
    }

    }

