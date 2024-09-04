using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pegarMeleca : MonoBehaviour

{
    public GameObject musica;
    public GameObject Bloqueio;
    public float contaMuco = 0;
    public Text txtmucopego;
    public GameObject mucopego;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pessoa")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Perdeu");
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lari")
        {
            Destroy(other.gameObject);
            contaMuco++;
            txtmucopego.text = "Muco pego: " + contaMuco;
        }
    }

    void OnTriggerStay(Collider other)    
    {
        if (other.gameObject.tag == "som")
        {
            musica.SetActive(true);
        }
    }

  void OnTriggerExit(Collider other)    
  {
        musica.SetActive(false);
   }

}

