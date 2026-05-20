using System.Collections.Generic;
using UnityEngine;

public class StudyPuzzle : Puzzle
{
    [SerializeField] List<GameObject> objectsToActivateWhenSolved = new();
    [SerializeField] List<GameObject> objectsToDeactivateWhenSolved = new();
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
    }
}
