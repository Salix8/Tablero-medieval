using UnityEngine;

public class GizmoVisualizer : MonoBehaviour
{
    // Este m�todo dibuja un gizmo todo el tiempo (tanto en Play Mode como en el Editor)
    void OnDrawGizmos()
    {
        // Cambiar el color del Gizmo
        Gizmos.color = Color.red;

        // Dibuja una esfera con un radio m�s grande (2.0)
        Gizmos.DrawSphere(transform.position, 20.0f);


        // Dibuja un cubo en la posici�n del objeto
        //Gizmos.color = Color.green;
        //Gizmos.DrawCube(transform.position, Vector3.one);  // Cubo de 1x1x1
    }
}
