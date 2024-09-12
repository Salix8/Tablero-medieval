using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ShakeEffect shakeEffect;
    World world;
    private int x, y; // Posición en el tablero (ref script SpawnWorld)
    bool isSelected = false;
    [SerializeField] int speed = 1;

        GameObject tablero;
    private void Start()
    {
        gameObject.AddComponent<ShakeEffect>();
        shakeEffect = GetComponent<ShakeEffect>();
        tablero = GameObject.FindGameObjectWithTag("World");
        world = tablero.gameObject.GetComponent<World>();
    }

    private void Update()
    {
        if (world == null)
            world = tablero.gameObject.AddComponent<World>();

        //if (Input.GetMouseButton(0))
        //{
        //    world.casillasDisponibles(speed);
        //}
    }

    //private void OnMouseDown()
    //{
    //    if (!isSelected) { // Esto quiere decir que ahora si que estará Seleccionado
    //        shakeEffect.StartShake();
    //        // Casillas disponibles
    //        world.casillasDisponibles(speed, isSelected);
    //        // Movimiento a casillas disponibles
    //        Debug.Log(world.posicionCasilla());
    //        transform.position = world.posicionCasilla(); // Posicion del jugador pasa a ser la de la casilla
    //        isSelected=true;
    //    }
    //    else
    //    {
    //        world.casillasDisponibles(speed, isSelected);
    //        isSelected =false;
    //    }
    //}
    private void OnMouseDown()
    {
        if (!isSelected)
        {
            shakeEffect.StartShake();
            // Casillas disponibles
            world.casillasDisponibles(speed, isSelected);
            // Movimiento a casilla seleccionada
            Vector3 targetPosition = world.posicionCasilla();
            StartCoroutine(MoveToPosition(targetPosition)); // Inicia una corrutina para mover al jugador
            isSelected = true;
        }
        else
        {
            world.casillasDisponibles(speed, isSelected);
            isSelected = false;
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

}
