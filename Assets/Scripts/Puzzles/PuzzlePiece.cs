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
        if (parentPuzzle != null) parentPuzzle.RegisterPiece(this);
    }

    protected virtual void OnDisable()
    {
        if (parentPuzzle != null) parentPuzzle.UnregisterPiece(this);
    }

    public abstract bool IsInCorrectState { get; }
    public abstract void Interact();

    public virtual bool CanInteract =>
        parentPuzzle == null || (parentPuzzle.IsActive && !parentPuzzle.IsSolved);

    protected void NotifyStateChanged() => OnStateChanged?.Invoke(this);
}
