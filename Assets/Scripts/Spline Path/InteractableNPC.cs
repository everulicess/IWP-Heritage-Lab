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
        interactText.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && !dialogueOpen && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        dialogueOpen = true;

        splineAnimate.Pause();

        interactText.SetActive(false);
        dialoguePanel.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueOpen = false;

        dialoguePanel.SetActive(false);
        splineAnimate.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (!dialogueOpen)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactText.SetActive(false);
        }
    }
}
