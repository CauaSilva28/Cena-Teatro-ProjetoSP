using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentoPerso : MonoBehaviour
{
    public Transform camera;

    private float velocidade;
    public float veloCorrendo;
    public float veloAndando;
    
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator anim;

    public Text gosma;
    public Text frasesTela;

    public GameObject somPassos;
    public GameObject somCorrendo;
    public GameObject somAndandoAgua;
    public AudioSource somTrancada;

    float rotationSpeed = 150f;

    private float gravidade = 9.8f;
    private float velocidadeVertical = 0f;

    public string textoBarragem;

    public bool emAreaDeFala;
    private bool naAgua = false;

    public bool pausado;
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
        if(!pausado){
            Movimentacao();
            Gravidade();
            Rotacao();
        }
        else{
            somPassos.SetActive(false);
            somAndandoAgua.SetActive(false);
            somCorrendo.SetActive(false);
        }
    }

    //Funções

    private void Movimentacao(){
        if(!emAreaDeFala){
            if(naAgua){
                velocidade = 4f;
                somPassos.SetActive(false);
                somCorrendo.SetActive(false);
            }
            else{
                somAndandoAgua.SetActive(false);
            }

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                anim.SetInteger("transition", 1); 
                if(!naAgua){
                    somPassos.SetActive(true);
                    velocidade = veloAndando;
                }
                else{
                    somAndandoAgua.SetActive(true);
                }
            }
            else{
                anim.SetInteger("transition", 0);
                somPassos.SetActive(false);
                somAndandoAgua.SetActive(false);
            }

            if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)){
                anim.SetInteger("transition", 2);
                somCorrendo.SetActive(true);       
                if(!naAgua){
                    somPassos.SetActive(false);
                    velocidade = veloCorrendo;
                }
                else{
                    somAndandoAgua.SetActive(true);
                }
            }
            else{
                somCorrendo.SetActive(false);
            }
        }
        else{
            anim.SetInteger("transition", 0);
            somPassos.SetActive(false);
            somAndandoAgua.SetActive(false);
            somCorrendo.SetActive(false);
            velocidade = 0;
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) ;
        moveDirection = camera.TransformDirection(moveDirection);
    }

    private void Gravidade(){
        if (controller.isGrounded)
        {
            velocidadeVertical = 0f;

            if(Input.GetKey(KeyCode.Space)){
                velocidadeVertical = 2f;
                anim.SetBool("pulando", true);
            }

            if(velocidadeVertical <= 0){
                anim.SetBool("pulando", false);
            }
        }
        else
        {
            velocidadeVertical -= gravidade * Time.deltaTime;
        }

        moveDirection.y = velocidadeVertical;

        controller.Move(moveDirection * velocidade * Time.deltaTime);

        // Gira o personagem para a direção do movimento
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            Vector3 targetDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 8);
        }
    }

    private void Rotacao(){
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, -rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D)){
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }

    //Fim funções

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Porta")){
            somTrancada.Play();
        }
        if(other.gameObject.CompareTag("Agua")){
            naAgua = true;
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
        if(other.gameObject.CompareTag("Agua")){
            naAgua = false;
        }
    }

    void OnCollisionStay(Collision collider){
        if(collider.gameObject.CompareTag("Barragem")){
            frasesTela.text = textoBarragem;
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
