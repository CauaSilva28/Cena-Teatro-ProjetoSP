using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausar : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool pausado = false;
    public bool perdendo = false;

    public MovimentoPerso movePerso;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!perdendo){
            if(pausado){
                if(Input.GetKeyDown(KeyCode.Escape)){
                    Despausar();
                }
            }
            else{
                if(Input.GetKeyDown(KeyCode.Escape)){
                    PauseJogo();
                }
            }
        }
    }

    void PauseJogo()
    {
        movePerso.pausado = true;
        pausado = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Despausar()
    {
        movePerso.pausado = false;
        pausado = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
