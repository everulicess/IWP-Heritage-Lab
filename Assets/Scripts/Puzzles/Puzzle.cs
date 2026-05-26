using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{

    [Header("Lights To Enable")]
    [SerializeField] private List<GameObject> roomLights;

    [SerializeField] protected PuzzleID puzzleId = PuzzleID.None;
    [SerializeField] protected bool startActive = true;

    public PuzzleState state = PuzzleState.None;

    public PuzzleID PuzzleId => puzzleId;
    public bool IsSolved { get; protected set; }
    public bool IsActive { get; protected set; }


    // Pieces register themselves � no inspector list to maintain
    readonly List<PuzzlePiece> pieces = new();
    public IReadOnlyList<PuzzlePiece> Pieces => pieces;
    public int PieceCount => pieces.Count;
    public int SolvedPieceCount
    {
        get
        {
            int count = 0;
            foreach (var p in pieces) if (p != null && p.IsInCorrectState) count++;
            return count;
        }
    }
    protected virtual void Awake()
    {
        GameManager.Instance.RegisterPuzzle(this);
    }

    public void RegisterPiece(PuzzlePiece piece)
    {
        if (piece == null || pieces.Contains(piece)) return;
        pieces.Add(piece);
        piece.OnStateChanged += HandlePieceChanged;
    }

    public void UnregisterPiece(PuzzlePiece piece)
    {
        if (piece == null) return;
        if (pieces.Remove(piece))
            piece.OnStateChanged -= HandlePieceChanged;
    }

    protected virtual void Start()
    {
        if (startActive) StartPuzzle();

        foreach (GameObject light in roomLights)
        {
            if (light != null)
            {
                light.SetActive(false);
            }
        }
    }

    public virtual void StartPuzzle()
    {
        if (IsSolved || IsActive) return;
        IsActive = true;
        state = PuzzleState.Started;
        OnStart();
        BroadcastState(state);
    }

    protected void Solve()
    {
        if (IsSolved) return;
        IsSolved = true;
        IsActive = false;
        state = PuzzleState.Solved;
        OnSolved();
        BroadcastState(state);
        Debug.Log("PUZZLE HAS BEEN SOLVED");

        foreach (GameObject light in roomLights)
        {
            if (light != null)
            {
                light.SetActive(true);
            }
        }
    }

    protected void Fail()
    {
        if (!IsActive) return;
        state = PuzzleState.Failed;
        OnFailed();
        BroadcastState(state);
    }

    protected virtual void HandlePieceChanged(PuzzlePiece piece) => EvaluateState();

    protected void EvaluateState()
    {
        if (!IsActive || IsSolved) return;
        if (CheckSolution()) Solve();
    }

    protected virtual bool CheckSolution()
    {
        if (pieces.Count == 0) return false;
        foreach (var p in pieces)
            if (p == null || !p.IsInCorrectState) return false;
        return true;
    }

    protected virtual void OnStart() { }
    protected virtual void OnSolved() { }
    protected virtual void OnFailed() { }

    protected void BroadcastState(PuzzleState state) =>
        EventsManager.Broadcast(new PuzzleStateChanged { puzzle = this, state = state });
}
