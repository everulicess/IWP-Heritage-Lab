using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Codex : MonoBehaviour
{
    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject pagePrefab;
    [SerializeField] GameObject buttonPrefab;

    [Space]
    [Header("Parents")]
    [SerializeField] GameObject pagesSection;
    [SerializeField] GameObject indexPage;

    [Space]
    [Header("Navigation")]
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;


    private List<CodexEntry> unlockedEntries = new();
    private List<GameObject> availablePages = new();
    private int currentIndex = 0;

    private void OnEnable()
    {
        EventsManager.AddListener<UnlockEntry>(OnUnlockEntry);
        EventsManager.AddListener<SelectEntry>(OnSelectEntry);
    }

    private void OnDisable()
    {
        EventsManager.RemoveListener<UnlockEntry>(OnUnlockEntry);
        EventsManager.RemoveListener<SelectEntry>(OnSelectEntry);
    }
    private void Start()
    {
        availablePages.Add(indexPage);
        ShowEntry(currentIndex);
    }
    void OnUnlockEntry(UnlockEntry evt)
    {
        Debug.Log($"UNLOCKING ENTRY {evt.Entry.name}");

        if (unlockedEntries.Contains(evt.Entry))
            return;
        //Instantiate Page
        GameObject _pageObject = Instantiate(pagePrefab, pagesSection.transform);
        _pageObject.GetComponent<Page>().SetInformation(evt.Entry);
        _pageObject.SetActive(false);
        availablePages.Add(_pageObject);
        //instantiate button index
        GameObject _indexButton = Instantiate(buttonPrefab, indexPage.transform);
        _indexButton.GetComponent<EntryButton>().SetPageIndex(evt.Entry, availablePages.Count - 1);
        unlockedEntries.Add(evt.Entry);
    }

    void OnSelectEntry(SelectEntry evt)
    {
        ShowEntry(evt.PageEntryIndex);
        currentIndex = evt.PageEntryIndex;
    }
    public void ShowEntry(int index)
    {
        foreach (var page in availablePages)
            page.gameObject.SetActive(false);

        availablePages[index].gameObject.SetActive(true);

        UpdateNavigationButtons(index);
    }

    private void UpdateNavigationButtons(int index)
    {
        bool hasEntry = index >= 0 && availablePages.Count > 0;

        previousButton.gameObject.SetActive(pagesSection.activeInHierarchy ? hasEntry && index > 0 : false);
        nextButton.gameObject.SetActive(pagesSection.activeInHierarchy ? hasEntry && index < availablePages.Count - 1 : false);
    }

    public void OnNextPage()
    {
        ShowEntry(++currentIndex);
    }
    public void OnPreviousPage()
    {
        ShowEntry(--currentIndex);
    }
    public void ToggleCodex()
    {
        pagesSection.SetActive(!pagesSection.activeInHierarchy);
        currentIndex =0;
        ShowEntry(currentIndex);

    }
}
