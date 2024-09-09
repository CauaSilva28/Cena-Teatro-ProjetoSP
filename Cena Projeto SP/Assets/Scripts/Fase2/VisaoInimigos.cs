using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisaoInimigos : MonoBehaviour
{
    public Light enemySpotlight;
    public Color originalColor = Color.white;
    public Color alertColor = Color.red;
    public float detectionRange = 10f;
    public GameObject player;
    public LayerMask LayersBloqueadas;

    public GameObject telaGameOver;
    public GameObject audios;
    public GameObject alarmePolicia;

    public Pausar pauseJogo;

    void Start()
    {
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
            StartCoroutine(Derrota());
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
        Vector3 directionToPlayer = player.GetComponent<Transform>().position - transform.position;

        // Verifica se o jogador está dentro do cone de visão do Spot Light
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < enemySpotlight.spotAngle / 2 && directionToPlayer.magnitude <= detectionRange)
        {
            // Verifica se há algum objeto na linha de visão que está em uma layer que bloqueia a visão
            if (!Physics.Linecast(transform.position, player.GetComponent<Transform>().position, LayersBloqueadas))
            {
                return true; // Retorna verdadeiro se não houver bloqueio
            }
        }

        return false; // Retorna falso se o jogador estiver fora da visão ou bloqueado
    }

    IEnumerator Derrota(){
        pauseJogo.perdendo = true;
        alarmePolicia.SetActive(true);
        player.GetComponent<MovimentoPerso>().emAreaDeFala = true;

        yield return new WaitForSeconds(3f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        telaGameOver.SetActive(true);
        audios.SetActive(false);
    }
}
