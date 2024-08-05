using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorCamera : MonoBehaviour
{
    public Transform alvo;
    public float ajusteCamera;


    private RaycastHit hit;
    private Vector3 initialPosition;


    void Start()
    {
        
    }

    void Update()
    {
        if (Physics.Linecast(alvo.position, transform.position, out hit))
        {
            // Se houver colisão, ajusta a posição da câmera
            transform.position = hit.point + transform.forward * ajusteCamera;
        }
    }
}
