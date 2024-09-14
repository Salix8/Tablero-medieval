using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    public string actionType;

    public bool isPlaying { get; private set; }

    private static MouseEvents instance;

    public void Awake()
    {
        isPlaying = true;
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un MouseEvents en la escena");
        }
        instance = this;
    }

    public static MouseEvents GetInstance()
    {
        return instance;
    }

    void OnMouseDown()
    {
        if(!isPlaying)
            return;

        Debug.Log("Des");
        switch (actionType){
            case "dialogue":
                StartDialogue();
                break;
            case "scene":
                ChangeScene();
                break;
            default:
                Debug.Log("Ninguna acción encontrada");
                break;
        }
    }

    void StartDialogue()
    {
        Debug.Log("Mostrando diálogo...");
    }

    void ChangeScene()
    {
        Debug.Log("Mostrando escena...");
    }

    public void MakeChoice(int choiceIndex)
    {
        Debug.Log("Hi num:" + choiceIndex);
    }
}