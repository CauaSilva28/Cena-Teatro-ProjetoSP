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

    public Text frasesTela;

    public GameObject somPassos;
    public GameObject somCorrendo;
    public GameObject somAndandoAgua;

    float rotationSpeed = 150f;

    private float gravidade = 9.8f;
    private float velocidadeVertical = 0f;

    public string textoBarragem;

    public bool emAreaDeFala;
    private bool naAgua = false;
    private bool pulou = false;

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
            // Chamando funcoes
            Movimentacao();
            Gravidade();
            Rotacao();

            moveDirection.y = velocidadeVertical;

            controller.Move(moveDirection * velocidade * Time.deltaTime); // Responsavel pelo controle(movimento) do personagem

            // Gira o personagem para a direcao do movimento
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                Vector3 targetDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 8);
            }
        }
        else{
            // Desativando sons de passos
            DesativarSomPassos();
        }
    }

    //Funcoes

    public void DesativarSomPassos(){
        somPassos.SetActive(false);
        somAndandoAgua.SetActive(false);
        somCorrendo.SetActive(false);
    }

    private void Movimentacao(){
        if(!emAreaDeFala){ // Faz com que o personagem nao se mova em areas de dialogo
            if(naAgua){ // Se o personagem estiver em algum local com a tag agua, executara esse comando
                velocidade = 4f;
                somPassos.SetActive(false);
                somCorrendo.SetActive(false);
            }
            else{
                somAndandoAgua.SetActive(false);
            }

            MovimentoAndando();
            MovimentoCorrendo();
        }
        else{
            anim.SetInteger("transition", 0);
            DesativarSomPassos();
            if(controller.isGrounded){
                velocidade = 0;
            }
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // Pega as os valores de movimentoa atraves das teclas W, A, S ,D, sendo A e D horizontal, W e S vertical
        moveDirection = camera.TransformDirection(moveDirection); // Move o jogador na direcao da camera
    }

    private void MovimentoAndando(){
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){ // Atribuindo animações e sons de passos ao clicar nas teclas de movimento
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
    }
    private void MovimentoCorrendo(){
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)){ // atribuindo animação e som correndo
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

    private void Gravidade(){
        if (controller.isGrounded) // Verifica se o personagem esta no chao
        {
            velocidadeVertical = 0f;

            if(!pulou && Input.GetKey(KeyCode.Space)){
                StartCoroutine(Pulo());
                pulou = true;
            } // Atribui a mecanica de pulo

            if(velocidadeVertical <= 0){
                anim.SetBool("pulando", false);
            }
        }
        else
        {
            // Aplica a gravidade no personagem
            velocidadeVertical -= gravidade * Time.deltaTime;
        }
    }

    IEnumerator Pulo(){
        velocidadeVertical = 2f;
        anim.SetBool("pulando", true);

        yield return new WaitForSeconds(1f);

        pulou = false;
    } // Coroutine e responsavel por trabalhar com intervalos, aqui atribui um intervalo de tempo entre os pulos

    private void Rotacao(){
        // Rotaciona o personagem 100% para a direcao da tecla apertada
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

    // Colisões

    void OnTriggerEnter(Collider other){ // colisores
        if(other.gameObject.CompareTag("Agua")){
            naAgua = true;
        }
    }
    
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Agua")){
            naAgua = false;
        }
    }

    void OnCollisionStay(Collision collider){ // colisoes
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
