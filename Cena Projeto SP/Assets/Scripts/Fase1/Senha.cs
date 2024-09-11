using UnityEngine;
using UnityEngine.UI;

public class Senha : MonoBehaviour
{
    public Button[] buttons; 
    public GameObject painelSenha;
    private string password = "231"; 
    private string input = ""; 
    public GameObject win;
    public GameObject lose;
    public GameObject porta;

    public Text frasesTeclas;

    private bool abrirTelaSenha;
    private bool telaAberta;

    public void Sair()
    {
        input = "";
        painelSenha.SetActive(false);
        telaAberta = false;
        Cursor.lockState = CursorLockMode.Locked;
        win.SetActive(false);
        lose.SetActive(false);
    }
    public void Reiniciar()
    {
        input = "";
        win.SetActive(false);
        lose.SetActive(false);
    }

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonNumber = i + 1;
            buttons[i].onClick.AddListener(() => OnButtonClicked(buttonNumber));
        }
    }

    void Update()
    {
        if(telaAberta){
            Cursor.lockState = CursorLockMode.None;
        }

        if (abrirTelaSenha)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                painelSenha.SetActive(true);
                telaAberta = true;
            }
        }
    }

    void OnButtonClicked(int buttonNumber)
    {
        input += buttonNumber.ToString();

        if (input.Length == 3)
        {
            if (input == password)
            {
                win.SetActive(true);
                porta.transform.eulerAngles = new Vector3(-90, 0, 90);
            }
            else
            {
                lose.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            frasesTeclas.enabled = true;
            frasesTeclas.text = "Aperte \"F\" para digitar a senha";
            abrirTelaSenha = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            frasesTeclas.enabled = false;
            frasesTeclas.text = "";
            abrirTelaSenha = false;
        }
    }
}