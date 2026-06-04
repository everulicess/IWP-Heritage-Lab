using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EntryButton : MonoBehaviour
{
    [Space]
    [Header("References")]
    [SerializeField] TextMeshProUGUI buttonNameHolder;

    private int pageIndex = 0;
    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Debug.LogError($"Button {gameObject.name} - Rect: {rect.rect}, Corners: {GetCorners()}");
    }
    public void SetPageIndex(CodexEntry entry, int _pageIndex)
    {
        pageIndex = _pageIndex;
        buttonNameHolder.text = entry.name;
    }

    public void PageSelected()
    {
        OnSelectEntry evt = new();
        evt.PageEntryIndex = pageIndex;
        EventsManager.Broadcast(evt);
    }
    

    Vector2[] GetCorners()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);
        return new Vector2[] { corners[0], corners[2] }; // bottom-left, top-right
    }

    void OnGUI()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        Debug.DrawLine(corners[0], corners[1], Color.red);
        Debug.DrawLine(corners[1], corners[2], Color.red);
        Debug.DrawLine(corners[2], corners[3], Color.red);
        Debug.DrawLine(corners[3], corners[0], Color.red);
    }
}
