using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncoesBotoes : MonoBehaviour
{
    public void Reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
