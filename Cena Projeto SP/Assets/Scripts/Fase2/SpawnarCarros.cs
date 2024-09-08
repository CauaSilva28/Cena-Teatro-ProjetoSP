using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnarCarros : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject[] carros;

    private Quaternion valorRotacao;

    private void Start(){
        InvokeRepeating("SpawnCarro", 1, 3);
    }

    void SpawnCarro(){
        int r = Random.Range(0, spawnpoints.Length);

        if(r == 0){
            valorRotacao = Quaternion.Euler(0f, 90f, 0f);
        }
        else{
            valorRotacao = Quaternion.Euler(0f, -90f, 0f);
        }
        
        int c = Random.Range(0, carros.Length);

        GameObject Carro = Instantiate(carros[c], spawnpoints[r].position, valorRotacao);
    }
}
