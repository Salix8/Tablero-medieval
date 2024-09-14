using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(BoxCollider2D))]
public class ColliderVisualizer2D : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        // Obtener el LineRenderer y el BoxCollider2D
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Configurar el LineRenderer
        lineRenderer.positionCount = 5;  // 4 esquinas + una adicional para cerrar el contorno
        lineRenderer.loop = false;  // No queremos que se cierre automáticamente
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 1.0f;
        lineRenderer.endWidth = 1.0f;

        // Asignar un material al LineRenderer
        if (lineRenderer.material == null)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }

        // Establecer el color
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // Dibuja el contorno del BoxCollider2D
        DrawBoxCollider2D();
    }

    void DrawBoxCollider2D()
    {
        // Obtener el tamaño y la posición del BoxCollider2D
        Vector2 size = boxCollider.size;
        Vector2 offset = boxCollider.offset;
        Vector3[] vertices = new Vector3[5];

        // Calcular las esquinas del BoxCollider2D en coordenadas mundiales
        vertices[0] = transform.TransformPoint(new Vector3(offset.x - size.x / 2, offset.y - size.y / 2, 0));  // Bottom Left
        vertices[1] = transform.TransformPoint(new Vector3(offset.x - size.x / 2, offset.y + size.y / 2, 0));  // Top Left
        vertices[2] = transform.TransformPoint(new Vector3(offset.x + size.x / 2, offset.y + size.y / 2, 0));  // Top Right
        vertices[3] = transform.TransformPoint(new Vector3(offset.x + size.x / 2, offset.y - size.y / 2, 0));  // Bottom Right
        vertices[4] = vertices[0];  // Cierra el contorno

        // Asignar los vértices al LineRenderer
        lineRenderer.SetPositions(vertices);
    }
}
