using UnityEngine;

public class ColliderVisualizer2D2 : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Obt�n el componente BoxCollider2D
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            // Establecer el color del Gizmo
            Gizmos.color = Color.red;

            // Obtener las dimensiones y la posici�n del BoxCollider2D
            Vector2 size = boxCollider.size;
            Vector2 center = boxCollider.offset;  // Para BoxCollider2D usamos 'offset' en lugar de 'center'

            // Dibuja un cubo 2D (en realidad un cuadrado) en la misma posici�n y con el mismo tama�o que el BoxCollider2D
            Gizmos.DrawWireCube((Vector2)transform.position + center, size);
        }
    }
}
