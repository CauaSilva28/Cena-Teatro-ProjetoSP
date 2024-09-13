using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColisoesFase4 : MonoBehaviour
{
    public Transform porta;
    public Transform portaCorredor;
    public int itemCount = 0;
    public int totalItems = 3;
    public GameObject espadaEstatua;
    public GameObject BlocoInvisivel;
    public Inimigo inimigo;
    public GameObject canvaJokenpo;
    public GameObject txtTeclaE;
    private bool naAreaJokenpo = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("caixa"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Perdeu");
        }
        if (other.gameObject.CompareTag("cubo"))
        {
            porta.eulerAngles = new Vector3(-90, 0, -90);
            Destroy(other.gameObject);
            BlocoInvisivel.SetActive(false);
        }
        if (other.gameObject.tag == "item")
        {
            Destroy(other.gameObject);
            itemCount++;

            if (itemCount >= totalItems)
            {
                AtivaEspada();
            }
        }
        if (other.gameObject.CompareTag("cmcMover"))
        {
            inimigo.podeMover = true;

        }
        if (other.gameObject.CompareTag("Jokenpo"))
        {
            txtTeclaE.SetActive(true);
            naAreaJokenpo = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jokenpo"))
        {
            txtTeclaE.SetActive(false);
            naAreaJokenpo = false;
        }
    }

    void Update()
    {
        if (naAreaJokenpo && Input.GetKeyDown(KeyCode.E))
        {
            canvaJokenpo.SetActive(true);
            txtTeclaE.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Start()
    {
        if (espadaEstatua != null)
        {
            espadaEstatua.SetActive(false);
        }
    }

    void AtivaEspada()
    {
        if (espadaEstatua != null)
        {
            espadaEstatua.SetActive(true);
            porta.eulerAngles = new Vector3(-90, 0, 0);
            portaCorredor.eulerAngles = new Vector3(-90, 0, 0);

        }
    }
    public void TrocarCenaReiniciar()
    {
        SceneManager.LoadScene("Fase4");

    }
}
