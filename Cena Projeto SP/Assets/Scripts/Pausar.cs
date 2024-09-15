using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausar : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool pausado = false;
    public bool perdendo = false;

    public MovimentoPerso movePerso;

    public Slider volumeJogo;

    // Start is called before the first frame update
    void Start()
    {
        if(AudioListener.volume == 0){
            AudioListener.volume = 1;
        }
        else{
            volumeJogo.value = AudioListener.volume;
        } // AudioListener e o volume geral do jogo, aqui foi igualado ao slider que esta sendo utilizado para determinar seu valor
    }

    // Update is called once per frame
    void Update()
    {
        if(!perdendo){
            AudioListener.volume = volumeJogo.value; // Determinando volume atráves do slider

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
        Time.timeScale = 0; // Responsável por deixar o tempo do jogo parado (= a 0);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Despausar()
    {
        movePerso.pausado = false;
        pausado = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Deixa o jogo no tempo normal (= a 1);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
