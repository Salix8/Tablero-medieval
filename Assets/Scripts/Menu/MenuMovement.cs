using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    [SerializeField] private float mousePosX;
    [SerializeField] private float mousePosY;
    [SerializeField] private float movementQuantity = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;

        this.GetComponent<RectTransform>().position = 
            new Vector2 ((mousePosX / Screen.width) * movementQuantity + (Screen.width / 2),
                         (mousePosY / Screen.height) * movementQuantity + (Screen.height / 2) );
        
    }
}
