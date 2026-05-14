using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class EntryUnlocker : MonoBehaviour
{
    [SerializeField] CodexEntry entry;
    InteractableObject interactableObject;
    private void OnEnable()
    {
        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteraction.AddListener(UnlockEntry);
    }
    void UnlockEntry()
    {
        Debug.Log($"UNLOCKING ENTRY {entry}");

        UnlockEntry evt = new();
        evt.Entry = entry;
        EventsManager.Broadcast(evt);
    }
}
