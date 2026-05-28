using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class EntryUnlocker : MonoBehaviour
{
    [SerializeField] CodexEntry entry;
    InteractableObject interactableObject;
    bool isUnlocked = false;
    private void OnEnable()
    {
        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteraction.AddListener(UnlockEntry);
    }
    void UnlockEntry()
    {
        if (isUnlocked)
            return;
        Debug.Log($"UNLOCKING ENTRY {entry}");
        isUnlocked = true;

        OnUnlockedEntry evt = new();
        evt.Entry = entry;
        EventsManager.Broadcast(evt);
    }
}
