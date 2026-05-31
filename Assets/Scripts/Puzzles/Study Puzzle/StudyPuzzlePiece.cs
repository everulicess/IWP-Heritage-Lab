using System;
using UnityEngine;

public class StudyPuzzlePiece : PuzzlePiece
{
    [SerializeField] CrestID correctCrestId;
    CrestID selectedCrestId;
     CrestID placedCrestId;
    public override bool IsInCorrectState
    {
        get 
        { 
            return placedCrestId == correctCrestId; 
        }
    }

    protected override void Awake()
    {
        base.Awake();
        EventsManager.AddListener<CarriedCrest>(SetCurrentCrest);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventsManager.RemoveListener<CarriedCrest>(SetCurrentCrest);
    }
    private void SetCurrentCrest(CarriedCrest evt)
    {
        selectedCrestId = evt.carriedID;
        Debug.Log($"new crest is set to {selectedCrestId}");
    }
    protected override void Interact()
    {
        if (!CanInteract) return;
        placedCrestId = selectedCrestId;
        NotifyStateChanged();

        if (IsInCorrectState)
        {
            Destroy(this);
            Destroy(interactable);
            EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = false });
        }
        else
        {
            
        }
    }
}
