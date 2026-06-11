using UnityEngine;
using UnityEngine.Splines;
using System;

public class InteractableNPC : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Animator Animator;
    private GameObject player;
    private bool playerNearby;
    private bool dialogueOpen;
    private AudioSource Source;


    private void Start()
    {
        //interactText.SetActive(false);
        dialoguePanel.SetActive(false);
        InputManager.Instance.UI.Spacebar.performed += ctx => CloseDialogue();
        Animator.SetBool("Is_Moving", true);
        player = GameObject.FindGameObjectWithTag("Player");
        Source = GetComponent<AudioSource>();
    }



    public void StartDialogue()
    {
        if (dialogueOpen)
            return;

        GameManager.Instance.SetState(GameState.Inspecting);
        
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; 
        transform.rotation = Quaternion.LookRotation(direction);

        Source.Play();

        Animator.SetBool("Is_Moving", false);

        dialogueOpen = true;

        splineAnimate.Pause();

        //interactText.SetActive(false);
        dialoguePanel.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueOpen = false;

        Animator.SetBool("Is_Moving", true);
        Source.Stop();

        dialoguePanel.SetActive(false);
        splineAnimate.Play();
        GameManager.Instance.SetState(GameState.Gameplay);
    }

}
