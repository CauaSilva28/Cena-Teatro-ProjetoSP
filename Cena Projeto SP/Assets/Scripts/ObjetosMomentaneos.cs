using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosMomentaneos : MonoBehaviour
{
    public GameObject objeto;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DesativarObjeto", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DesativarObjeto(){
        objeto.SetActive(false);
    }
}
