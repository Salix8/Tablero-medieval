using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestructorMouse : MonoBehaviour
{
    
    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(this.gameObject);
        Debug.Log("Se ha destruido el GameObject: " + this.gameObject.name);
    }

}
