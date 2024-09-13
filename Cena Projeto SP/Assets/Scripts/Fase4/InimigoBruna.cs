using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Transform[] pontosCaminho;  
    public float speed = 5f;
    private int pontoAtual = 0;  
    private bool chegouNoUltimoPonto = false;
    public bool podeMover = false;  
    private void Update()
    {
        if (podeMover && pontoAtual < pontosCaminho.Length)
        {
            moveAtePonto(pontosCaminho[pontoAtual]);

            if (Vector3.Distance(transform.position, pontosCaminho[pontoAtual].position) < 0.1f)
            {
                pontoAtual++;
                if (pontoAtual == pontosCaminho.Length && !chegouNoUltimoPonto)
                {
                    transform.Rotate(0, 180, 0);
                    chegouNoUltimoPonto = true; 
                }
            }
        }
    }

    private void moveAtePonto(Transform targetPoint)
    {
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(targetPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cmcMover"))
        {
            podeMover = true;  // Ativa o movimento
        }
    }
}