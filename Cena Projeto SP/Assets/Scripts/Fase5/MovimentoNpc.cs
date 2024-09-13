using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentoNpc : MonoBehaviour
{
    private NavMeshAgent navMesh;
    public float distanciaMinima;
    public float velocidade;
    public Transform player;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.stoppingDistance = distanciaMinima;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.SetDestination(player.position);

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia <= distanciaMinima)
        {
            navMesh.speed = 0;
            anim.SetBool("andando", false);

        }
        else
        {
            navMesh.speed = velocidade;
            anim.SetBool("andando", true);
        }
    }
}
