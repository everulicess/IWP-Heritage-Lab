using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class StartingGate : MonoBehaviour
{
    InteractableObject interactable;
    bool gateOpened;
    private void OnEnable()
    {
        EventsManager.AddListener<OnGameStateChanged>(OnGameStateChanged);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnGameStateChanged>(OnGameStateChanged);

    }
    void Start()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteraction.AddListener(Interact);
    }
    void OnGameStateChanged(OnGameStateChanged evt)
    {
        gateOpened = evt.NewState == GameState.Finished;
    }
    void Interact()
    {
        EventsManager.Broadcast(new OnGateInteraction{ GateOpened = gateOpened });
    }

}
