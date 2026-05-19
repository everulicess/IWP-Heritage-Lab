using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{

    [Header("Tutorial Flow")]
    public TutorialStep[] steps;

    [Tooltip("Show tutorial text UI (optional)")]
    public GameObject tutorialBackdrop;

    private int currentStepIndex = -1;
    private bool waitingForClick = false;

    public UnityEvent onTutorialFinished;
    public UnityEvent onTutorialStarted;

    Coroutine _autoNextCoroutine;
    private void OnEnable()
    {
        InputManager.Instance.UI.Click.performed += ctx => OnClickNext();
        InputManager.Instance.UI.SkipTutorial.performed += ctx => SkipTutorial();
        EventsManager.AddListener<OnNotifyTutorialStepEvent>(NotifyStepEvent);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnNotifyTutorialStepEvent>(NotifyStepEvent);
    }
    void Start()
    {
        StartTutorial();
    }
    public void StartTutorial()
    {
        Debug.Log("TUTORIAL STARTED");
        if (steps.Length == 0)
        {
            Debug.LogWarning("No tutorial steps assigned!");
            return;
        }
        onTutorialStarted?.Invoke();
        GoToStep(0);
    }

    public void GoToStep(int index)
    {
        if (index < 0 || index >= steps.Length) return;

        // Exit previous step
        if (currentStepIndex >= 0 && currentStepIndex < steps.Length)
            steps[currentStepIndex].ExitStep();

        currentStepIndex = index;
        TutorialStep step = steps[currentStepIndex];
        step.EnterStep();

        if (tutorialBackdrop)
        {
            tutorialBackdrop.SetActive(step.ShowBackdrop);
        }

        if (step.autoContinue)
        {
            if (_autoNextCoroutine != null) StopCoroutine(_autoNextCoroutine);
            _autoNextCoroutine = StartCoroutine(AutoNext(step.autoContinueDelay));
        }
        else if (step.waitForPlayerClick)
        {
            waitingForClick = true;
        }
        else if (!step.waitForEvent)
        {
            // If no waiting condition, go immediately
            GoToNextStep();
        }
    }

    private IEnumerator AutoNext(float delay)
    {
        yield return new WaitForSeconds(delay);
        GoToNextStep();
    }

    void OnClickNext()
    {
        if (!waitingForClick)
            return;
        waitingForClick = false;
        GoToNextStep();

    }
    public void GoToNextStep()
    {
        if (currentStepIndex + 1 < steps.Length)
            GoToStep(currentStepIndex + 1);
        else
            EndTutorial();
    }
    public void EndTutorial()
    {
        if (tutorialBackdrop)
            tutorialBackdrop.SetActive(false);

        onTutorialFinished?.Invoke();
        Debug.Log("tutorial finished");
    }
    public void NotifyStepEvent(OnNotifyTutorialStepEvent evt)
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Length) return;

        TutorialStep step = steps[currentStepIndex];
        if (step.stepName != evt.stepToNotify || step.isCompleted) return;

        step.amountOfEvents--;
        if (step.amountOfEvents <= 0)
            GoToNextStep();
    }
    public void SkipTutorial()
    {
        for (int i = 0; i < steps.Length; i++)
            GoToStep(i);
        GoToNextStep(); //ends the tutorial
    }
    public void RestartTutorial()
    {
        GoToStep(0);
    }

}


[Serializable]
public class TutorialStep
{
    [Header("Step Info")]
    public string stepName;

    [Header("UI Elements")]
    [SerializeField] GameObject[] objectsToActivate;
    [SerializeField] GameObject[] objectsToDeactivate;
    [Tooltip("If true, shows the tutorial backdrop in the middle of the screen.")]
    public bool ShowBackdrop = true;

    [Header("Conditions")]
    [Tooltip("if true, waits for the player to click anywhere.")]
    public bool waitForPlayerClick;
    [Tooltip("if true, waits for the 'TutorialStepTrigger' script to fire the event using the same name of the step.")]
    public bool waitForEvent;
    [Tooltip("Number of actions to go to the next step.")]
    public int amountOfEvents = 1;

    [Tooltip("If true, automatically continues to next step after delay.")]
    public bool autoContinue;
    public float autoContinueDelay = 1f;

    [Header("Events")]
    [SerializeField]
    [Tooltip("You can add extra functions to perform at the start of the step. (play animations, sounds, etc.)")]
    UnityEvent onStepStart;
    [SerializeField]
    [Tooltip("You can add extra functions to perform at the end of the step. (play animations, sounds, etc.)")]
    UnityEvent onStepComplete;

    [HideInInspector] public bool isCompleted = false;

    public void EnterStep()
    {
        foreach (var go in objectsToActivate)
            if (go) go.SetActive(true);

        foreach (var go in objectsToDeactivate)
            if (go) go.SetActive(false);

        onStepStart?.Invoke();
        isCompleted = false;
    }
    public void ExitStep()
    {
        onStepComplete?.Invoke();
        isCompleted = true;
    }
}