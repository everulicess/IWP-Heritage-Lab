using UnityEngine;
using UnityEngine.Splines;
using System;

public class InteractableNPC : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject dialoguePanel;

    private bool playerNearby;
    private bool dialogueOpen;

    private void Start()
    {
        //interactText.SetActive(false);
        dialoguePanel.SetActive(false);
        InputManager.Instance.UI.Spacebar.performed += ctx => CloseDialogue();
    }



    public void StartDialogue()
    {
        if (dialogueOpen)
            return;

        GameManager.Instance.SetState(GameState.Inspecting);

        dialogueOpen = true;

        splineAnimate.Pause();

        //interactText.SetActive(false);
        dialoguePanel.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueOpen = false;

        dialoguePanel.SetActive(false);
        splineAnimate.Play();
        GameManager.Instance.SetState(GameState.Gameplay);
    }

}
