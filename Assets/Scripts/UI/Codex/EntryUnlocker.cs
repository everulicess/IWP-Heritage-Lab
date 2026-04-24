using UnityEngine;

public class EntryUnlocker : MonoBehaviour
{
    [SerializeField] CodexEntry entry;
    public void UnlockEntry()
    {
        Debug.Log($"UNLOCKING ENTRY {entry}");

        UnlockEntry evt = new();
        evt.Entry = entry;
        EventsManager.Broadcast(evt);
    }
}
