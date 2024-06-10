using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentoPerso : MonoBehaviour
{
    private float velocidade = 5f;
    
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator anim;

    public Text gosma;
    public Text frasesTela;

    public GameObject somPassos;
    public AudioSource somTrancada;

    float rotationSpeed = 150f;

    private float gravidade = 9.8f;
    private float velocidadeVertical = 0f;

    public bool emAreaDeFala;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!emAreaDeFala){
            velocidade = 5f;
            //Movimentação-------------------------------
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
                anim.SetInteger("transition", 1);
                somPassos.SetActive(true);
            }
            else{
                anim.SetInteger("transition", 0);
                somPassos.SetActive(false);
            }
        }
        else{
            anim.SetInteger("transition", 0);
            somPassos.SetActive(false);
            velocidade = 0;
        }

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical")) * velocidade;
        moveDirection = transform.TransformDirection(moveDirection);

        //Gravidade-----------------------------
        if (controller.isGrounded)
        {
            velocidadeVertical = 0f; // Reseta a velocidade vertical se estiver no chão
        }
        else
        {
            velocidadeVertical -= gravidade * Time.deltaTime; // Aplica a gravidade
        }

        moveDirection.y = velocidadeVertical;
        //FIM Gravidade-----------------------------
    
        controller.Move(moveDirection * Time.deltaTime);
        
        Vector3 posicaoAtual = transform.position;

        if (posicaoAtual.y > 9.85f)
        {
            posicaoAtual.y = 9.85f;
            transform.position = posicaoAtual;
        }
        //FIM Movimentação-------------------------------

        //Rotação-------------------------------
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, -rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D)){
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        //FIM Rotação-------------------------------
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Porta")){
            somTrancada.Play();
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("GosmaRa")){
            gosma.enabled = true;
        }
        if(other.gameObject.CompareTag("Porta")){
            frasesTela.text = "Essa sala está trancada";
            frasesTela.enabled = true;
        }
    }
    
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("GosmaRa")){
            gosma.enabled = false;
        }
        if(other.gameObject.CompareTag("Porta")){
            frasesTela.text = "";
            frasesTela.enabled = false;
        }
    }

    void OnCollisionStay(Collision collider){
        if(collider.gameObject.CompareTag("Barragem")){
            frasesTela.text = "Fale com o atendente";
            frasesTela.enabled = true;
        }
    }

    void OnCollisionExit(Collision collider){
        if(collider.gameObject.CompareTag("Barragem")){
            frasesTela.text = "";
            frasesTela.enabled = false;
        }
    }
}
