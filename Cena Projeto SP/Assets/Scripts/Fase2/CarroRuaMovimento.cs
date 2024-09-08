using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroRuaMovimento : MonoBehaviour
{
    private float veloCarro = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        veloCarro = 50 * Time.deltaTime;
        Vector3 carroAndando = new Vector3(0f,0f,veloCarro);
        transform.Translate(carroAndando);

        if(transform.position.x > 460f || transform.position.x < -400f){
            Destroy(gameObject);
        }
    }
}
