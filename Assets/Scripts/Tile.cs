using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    Color predeterminado;
    [SerializeField] float highLight = 1.3f;
    public bool isDisponible = false;

    void Awake()
    {
        predeterminado = meshRenderer.materials[0].color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor()
    {
        meshRenderer.materials[0].color = predeterminado * highLight;
    }

    public void RevertColor()
    {
        meshRenderer.materials[0].color = predeterminado;
    }

    private void OnMouseDown()
    {
        if (isDisponible)
        {

        }
    }
}
