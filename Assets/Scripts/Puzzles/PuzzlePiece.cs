using System;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public abstract class PuzzlePiece : MonoBehaviour
{
    [SerializeField] protected Puzzle parentPuzzle;
    public Puzzle ParentPuzzle => parentPuzzle;

    public event Action<PuzzlePiece> OnStateChanged;

    protected InteractableObject interactable;


    protected virtual void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteraction.AddListener(Interact);
        if (parentPuzzle == null) parentPuzzle = GetComponentInParent<Puzzle>();
    }

    protected virtual void OnEnable()
    {
        if (parentPuzzle == null)
            return;
        parentPuzzle.RegisterPiece(this);
        EventsManager.AddListener<OnPuzzleStateChanged>(UpdatePiece);
    }

    protected virtual void OnDisable()
    {
        if (parentPuzzle == null)
            return;

        parentPuzzle.UnregisterPiece(this);
        EventsManager.RemoveListener<OnPuzzleStateChanged>(UpdatePiece);

    }

    private void UpdatePiece(OnPuzzleStateChanged evt)
    {
        if (evt.puzzle != parentPuzzle)
            return;
        interactable.canBeInteracted = evt.state != PuzzleState.Solved;
        EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = false });
    }

    public abstract bool IsInCorrectState { get; }
    protected virtual void Interact()
    {
        EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = interactable.canBeInteracted });

    }

    public virtual bool CanInteract =>
        parentPuzzle == null || (parentPuzzle.IsActive && !parentPuzzle.IsSolved);

    protected void NotifyStateChanged() => OnStateChanged?.Invoke(this);
}
