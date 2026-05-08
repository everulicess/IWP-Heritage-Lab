using UnityEngine;

public class TutorialStepTrigger : MonoBehaviour
{
    [Tooltip("Write down the name of the step to trigger. \n Must match exactly the name in the tutorial.")]
    [SerializeField]
    string stepNameToNotify;

    public void TriggerStep(string stepName = "")
    {
        if (string.IsNullOrEmpty(stepName))
            return;

        OnNotifyTutorialStepEvent evt = new();
        evt.stepToNotify = stepName;
        EventsManager.Broadcast(evt);
    }
}
