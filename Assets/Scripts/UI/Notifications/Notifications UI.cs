using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class NotificationsUI : MonoBehaviour
{
    [SerializeField] NotificationInfo[] notificationPresets;
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] VerticalLayoutGroup verticalGroupLayout;

    private void OnEnable()
    {
        EventsManager.AddListener<OnPuzzleStateChanged>(CreatePuzzleNotification);

        EventsManager.AddListener<OnEntryAdded>(CreateEntryNoticfication);

        if (notificationPrefab == null)
            Debug.LogError($"MISSING REFRENCE FOR {notificationPrefab.name} IN {this.gameObject}");
        if (verticalGroupLayout == null)
            Debug.LogError($"MISSING REFRENCE FOR {verticalGroupLayout.name} IN {this.gameObject}");
        if (notificationPresets.Count() <= 0)
            Debug.LogError($"MISSING REFRENCEs FOR {notificationPresets} IN {this.gameObject}");
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnEntryAdded>(CreateEntryNoticfication);
        EventsManager.RemoveListener<OnPuzzleStateChanged>(CreatePuzzleNotification);

    }

    private void CreatePuzzleNotification(OnPuzzleStateChanged evt)
    {
        if (evt.state != PuzzleState.Solved)
            return;
        CreateNotification(NotificationCategory.PuzzleFinished, $"{evt.puzzle.PuzzleId} Solved!");
    }
    private void CreateEntryNoticfication(OnEntryAdded evt)
    {
        if (evt.Entry.isRelic)
            CreateNotification(NotificationCategory.RelicCollected, $"{evt.Entry.title}!");
        CreateNotification(NotificationCategory.EntryUnlocked, $"{evt.Entry.title}, Check the Codex!");
    }
    private void CreateNotification(NotificationCategory category ,string displayText = "")
    {
        if (notificationPrefab == null)
            return;
        GameObject noti = Instantiate(notificationPrefab, verticalGroupLayout.transform);
        NotificationInfo infoToPass = GetNotificationPresets(category);
        infoToPass.textToDisplay = displayText;
        noti.GetComponent<Notification>().InitializeNotification(infoToPass);
    }
    NotificationInfo GetNotificationPresets(NotificationCategory category)
    {
        for (int i = 0; i < notificationPresets.Length; i++)
            if (notificationPresets[i].Categoy == category)
                return notificationPresets[i];
            
        NotificationInfo info1 = new();
        return info1; 
        
    }
}
