using UnityEngine;

public class LightPuzzle : Puzzle
{
    [Header("Bed States")]
    [SerializeField] GameObject inactiveBed;
    [SerializeField] GameObject activeBed;
    [SerializeField] GameObject finalRelic;
    [SerializeField] GameObject studyTempDoors;

    private TutorialStepTrigger tutorialStepTrigger;

    
    protected override void Awake()
   {
        base.Awake();
        tutorialStepTrigger = GetComponent<TutorialStepTrigger>();
   }
    protected override void OnStart()
    {
        // Initial state
        if (inactiveBed != null)
        {
            inactiveBed.SetActive(true);
        }

        if (activeBed != null)
        {
            activeBed.SetActive(false);
            finalRelic.SetActive(false);
        }
    }

    protected override void OnSolved()
    {
        base.OnSolved();
        // Swap models when solved
        if (inactiveBed != null)
        {
            inactiveBed.SetActive(false);
        }

        if (activeBed != null)
        {
            activeBed.SetActive(true);
            finalRelic.SetActive(true);
        }

  	// Deactivate temp doors in study
        if (studyTempDoors != null)
        {
            studyTempDoors.SetActive(false);
        }


        tutorialStepTrigger.TriggerStep();

        Debug.Log("Light puzzle solved");
    }
}