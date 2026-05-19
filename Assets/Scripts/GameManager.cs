using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics.Tracing;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    GameState currentState { get;  set; }
    GameState previousState;

    readonly List<Puzzle> puzzles = new();

    int SolvedPuzzlesCount;
   
    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventsManager.AddListener<OnCodexOpened>(OnCodexOpened);
        EventsManager.AddListener<OnCodexClosed>(OnCodexClosed);
        EventsManager.AddListener<PuzzleStateChanged>(OnPuzzleStateChanged);
        EventsManager.AddListener<OnGateInteraction>(OnWinningCheckCondition);
        SetState(GameState.Gameplay);

    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnCodexOpened>(OnCodexOpened);
        EventsManager.RemoveListener<OnCodexClosed>(OnCodexClosed);
        EventsManager.RemoveListener<PuzzleStateChanged>(OnPuzzleStateChanged);
        EventsManager.RemoveListener<OnGateInteraction>(OnWinningCheckCondition);


    }
    public void SetState(GameState newState)
    {
        previousState = currentState;
        //Debug.Log($"CURRENT GAME STATE {newState}");
        currentState = newState;

        Time.timeScale = newState == GameState.Paused ? 0.0f : 1.0f;

        EventsManager.Broadcast(new OnGameStateChanged { NewState = newState, PreviousState = previousState });
    }
    public void TogglePause()
    {
        if (currentState == GameState.Paused)
            SetState(previousState);
        else
            SetState(GameState.Paused);
    }
    void OnCodexOpened(OnCodexOpened evt)
    {
        SetState(GameState.Codex);
    }
    void OnCodexClosed(OnCodexClosed evt)
    {
        SetState(GameState.Gameplay);
        Debug.Log($"PREVIOUS GAME STATE {previousState}");

    }

    public void RegisterPuzzle(Puzzle piece)
    {
        if (piece == null || puzzles.Contains(piece)) return;
        puzzles.Add(piece);
    }

    private void OnPuzzleStateChanged(PuzzleStateChanged evt)
    {
        if (evt.state == PuzzleState.Solved)
            SolvedPuzzlesCount++;
        if (puzzles.Count == SolvedPuzzlesCount)
            SetState(GameState.Finished);
    }
    private void OnWinningCheckCondition(OnGateInteraction evt)
    {
        if (evt.GateOpened)
            SetState(GameState.WinningScreen);
    }
}
