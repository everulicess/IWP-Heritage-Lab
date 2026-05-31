using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StudyPuzzle : Puzzle
{
    [SerializeField] List<GameObject> objectsToActivateWhenSolved = new();
    [SerializeField] List<GameObject> objectsToDeactivateWhenSolved = new();
    [SerializeField] TextMeshProUGUI feedbackText;
    private TutorialStepTrigger tutorialStepTrigger;
    private InteractableObject interactable;


    protected override void Awake()
    {
        base.Awake();
        tutorialStepTrigger = GetComponent<TutorialStepTrigger>();
        interactable = GetComponent<InteractableObject>();

        if (objectsToActivateWhenSolved.Count <= 0 )
            return;

        foreach (var item in objectsToActivateWhenSolved)
            item.SetActive(false);
    }
    protected override void OnSolved()
    {
        base.OnSolved();
        feedbackText.text = "";
        tutorialStepTrigger.TriggerStep();
        interactable.Interact();

        if (objectsToActivateWhenSolved.Count <= 0)
            return;
        foreach (var item in objectsToActivateWhenSolved)
            item.SetActive(true);

        if (objectsToDeactivateWhenSolved.Count <= 0)
            return;
        foreach (var item in objectsToDeactivateWhenSolved)
            item.SetActive(false);

    }
    protected override void OnFailed()
    {

        base.OnFailed();
        StartCoroutine(FeedbackRoutine());
    }
    IEnumerator FeedbackRoutine()
    {
        feedbackText.text = $"Wrong Crest!";
        yield return new WaitForSeconds(1.0f);
        feedbackText.text = $"";

    }
}
