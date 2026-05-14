using UnityEngine;
public enum GameState
{
    Gameplay,
    Codex,
    Paused,
    Cutscene,
    Tutorial
}

#region PUZZLES
public enum PuzzleID
{
    None,
    StatuePuzzle,
    ExamplePuzzle,
    LightPuzzle
}
public enum PuzzleState
{
    Started,
    Solved,
    Failed
}
#endregion
