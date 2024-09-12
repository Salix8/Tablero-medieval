using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] float verticalMovment;
    [SerializeField] float horizontalMovment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalMovment = Input.GetAxis("Vertical");
        horizontalMovment = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalMovment);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalMovment);
    }
}
