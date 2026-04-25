using TMPro;
using UnityEngine;

public class EntryButton : MonoBehaviour
{
    [Space]
    [Header("References")]
    [SerializeField] TextMeshProUGUI buttonNameHolder;

    private int pageIndex = 0;
    public void SetPageIndex(CodexEntry entry, int _pageIndex)
    {
        pageIndex = _pageIndex;
        buttonNameHolder.text = entry.name;
    }

    public void PageSelected()
    {
        SelectEntry evt = new();
        evt.PageEntryIndex = pageIndex;
        EventsManager.Broadcast(evt);
    }
    
}
