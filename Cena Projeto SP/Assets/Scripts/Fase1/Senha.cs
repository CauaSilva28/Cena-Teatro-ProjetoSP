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

    void Sair()
    {
        painelSenha.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
            input = "";
        }
    }
}