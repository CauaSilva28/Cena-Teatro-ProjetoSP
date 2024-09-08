using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosPoliciaisAnim : MonoBehaviour
{
    public void Parar(){
        gameObject.GetComponent<Animator>().SetBool("parado", true);
        gameObject.GetComponent<Animator>().SetBool("andando", false);
    }

    public void Andando(){
        gameObject.GetComponent<Animator>().SetBool("parado", false);
        gameObject.GetComponent<Animator>().SetBool("andando", true);
    }
}
