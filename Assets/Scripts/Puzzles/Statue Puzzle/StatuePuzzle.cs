using UnityEngine;

public class StatuePuzzle : Puzzle
{
    private TutorialStepTrigger tutorialStepTrigger;
        [SerializeField] GameObject hudObjective;
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

        // Deactivate hud objective
        if (hudObjective != null)
        {
            hudObjective.SetActive(false);
        }
    }
}
