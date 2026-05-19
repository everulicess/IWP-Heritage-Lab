using UnityEngine;

public class StatuePuzzle : Puzzle
{
    private TutorialStepTrigger tutorialStepTrigger;
   protected override void Awake()
   {
        base.Awake();
        tutorialStepTrigger = GetComponent<TutorialStepTrigger>();
   }
   protected override void OnSolved()
    {
        base.OnSolved();
        tutorialStepTrigger.TriggerStep();
        Debug.Log("Statue Puzzle Solved");
    }
}
