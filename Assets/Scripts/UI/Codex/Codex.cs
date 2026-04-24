using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Codex : MonoBehaviour
{
    [SerializeField] GameObject pagesSection;
    [SerializeField] GameObject pagePrefab;
    private List<CodexEntry> unlockedEntries = new();

    private void OnEnable()
    {
        EventsManager.AddListener<UnlockEntry>(UnlockEntry);
    }

    private void OnDisable()
    {
        EventsManager.RemoveListener<UnlockEntry>(UnlockEntry);
    }
    void UnlockEntry(UnlockEntry evt)
    {
        Debug.Log($"UNLOCKING ENTRY {evt.Entry.name}");

        if (unlockedEntries.Contains(evt.Entry))
            return;
        GameObject _page = Instantiate(pagePrefab, pagesSection.transform);
        _page.GetComponent<Page>().SetInformation(evt.Entry);
        unlockedEntries.Add(evt.Entry);
    }

    public void ToggleCodex()
    {
        pagesSection.SetActive(!pagesSection.activeInHierarchy);
    }
}
