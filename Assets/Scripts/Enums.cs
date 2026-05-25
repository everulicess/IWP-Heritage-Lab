using UnityEngine;
public enum GameState
{
    Gameplay,
    Codex,
    Paused,
    Cutscene,
    Tutorial,
    Finished,
    WinningScreen
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
    None,
    Started,
    Solved,
    Failed
}
//StudyPuzzle
public enum CrestID
{
    None,
    Horenken,
    Alberda,
    Bentinck,
    VanWijhe,
    VanReede,
    VanNassau,
    DeWraat,
    VanBrederode,
    VanDeelen,
    VanRaesfeld,
    Ripperda,
    Rengers,
    Clant,
    Berum,
    Tamminga,
}
#endregion

public enum PlayerToolsID
{
    None,
    Flashlight,
    EMFDetector
}
