using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("DialogueManager UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    
    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un DialogueManager en la escena");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get the layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // Get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];

        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying) { // Solo queremos que se actualice si hay algun dialogo
            return;
        }

        if (currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Reset portrait, layout and speaker
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        Debug.Log("0: " + dialogueText.text);
        ContinueStory();
        Debug.Log("4: " + dialogueText.text);
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set text la linea actual del dialogo
            Debug.Log("1: " + dialogueText.text);
            dialogueText.text = currentStory.Continue();
            Debug.Log("2: " + dialogueText.text);
            //
            DisplayChoices();
            //
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine( ExitDialogueMode() );
        }
        Debug.Log("3: " + dialogueText.text);
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2 ) 
            {
                Debug.LogError("TAG Puede que no se haya parseado bien esta tag: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    //Debug.Log("Speaker= " + tagValue);
                    displayNameText.text = tagValue;
                    break;
                case PORTAIT_TAG: 
                    //Debug.Log("Portrait= " + tagValue); 
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG: 
                    //Debug.Log("Layout= " + tagValue);
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("La tag ha llegado, pero no se está gestionando correctamente: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        { 
            // Si ha saltado este error prueba a mirar el Dialoguemanager o los DialogueChoices del canvas
            Debug.LogError("Hay mas opciones de las que puede soportar la UI. Numero de opciones: " + currentChoices.Count);
        }

        // Activa las opciones necesarias
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // Oculta las opciones que sobran
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine( SelectFirstChoice() );
    }

    private IEnumerator SelectFirstChoice()
    {
        // EventSystem requiere que se borre primero y luego ya se puede establecer en un frame distinto
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager.GetInstance().RegisterSubmitPressed();
        ContinueStory();
    }
}
