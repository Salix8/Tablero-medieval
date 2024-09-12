using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRecolor : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    Color predeterminado;
    [SerializeField] float highLight = 1.3f;

    void Start()
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
}
