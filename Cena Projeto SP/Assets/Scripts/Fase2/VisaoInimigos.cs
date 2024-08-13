using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisaoInimigos : MonoBehaviour
{
    public Light enemySpotlight; // Spot Light do inimigo
    public Color originalColor = Color.white; // Cor original da luz
    public Color alertColor = Color.red; // Cor de alerta (vermelha)
    public float detectionRange = 10f; // Alcance da visão do inimigo
    public Transform player; // Referência ao jogador

    void Start()
    {
        // Define a cor original do Spot Light
        if (enemySpotlight != null)
        {
            enemySpotlight.color = originalColor;
        }
    }

    void Update()
    {
        // Verifica se o jogador está dentro do alcance da visão do inimigo
        if (PlayerNaArea())
        {
            // Muda a cor do Spot Light para vermelho
            enemySpotlight.color = alertColor;
        }
        else
        {
            // Volta a cor original do Spot Light
            enemySpotlight.color = originalColor;
        }
    }

    bool PlayerNaArea()
    {
        // Calcula a direção do inimigo para o jogador
        Vector3 directionToPlayer = player.position - transform.position;

        // Verifica se o jogador está dentro do cone de visão do Spot Light
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < enemySpotlight.spotAngle / 2 && directionToPlayer.magnitude <= detectionRange)
        {
            return true;
        }
        return false;
    }
}
