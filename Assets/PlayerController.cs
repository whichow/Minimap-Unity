using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveScale = 5f;
    public float rotScale = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * moveScale;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * moveScale;
        }
        // if(Input.GetKey(KeyCode.A))
        // {
        //     transform.position -= transform.right * Time.deltaTime * moveScale;
        // }
        // if(Input.GetKey(KeyCode.D))
        // {
        //     transform.position += transform.right * Time.deltaTime * moveScale;
        // }
        if(Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles -= transform.up * Time.deltaTime * rotScale;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles += transform.up * Time.deltaTime * rotScale;
        }
    }
}
