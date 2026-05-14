using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    //These are events that will be fired by the respective scripts
    //This references can be used for firing an event with no information at all (no changed variables)
    //public static NewStructure NewStructureEvent = new();
    //public static NewInstitution NewInstitutionEvent = new();
    //public static TestingEvent Testing = new();
}
//If there's any specific information to be passed through these events
//Add a public variable for it
//Change the value of said variables before firing the event in the corresponding script

//When anything that changes the map in any way happens, call this event
public class ExampleEvent : Event
{
    
}

#region GAME STATE EVENTS
public class OnGameStateChanged : Event
{
    public GameState NewState;
    public GameState PreviousState;
}
#endregion

#region CODEX EVENTS 
//CODEX EVENTS
public class UnlockEntry : Event
{
    public CodexEntry Entry;
}
public class SelectEntry : Event
{
    public int PageEntryIndex;
}
public class OnCodexOpened : Event
{
}
public class OnCodexClosed : Event
{
}

#endregion

#region TUTORIAL EVENTS
public class OnNotifyTutorialStepEvent : Event
{
    public string stepToNotify;
}

#endregion

#region PUZZLE EVENTS
public class PuzzleStateChanged : Event
{
    public Puzzle puzzle;
    public PuzzleState state;
}
//Srudy Puzzle
public class CarriedCrest : Event
{
    public CrestID carriedID = CrestID.None;
}
#endregion

public class OnGateInteraction : Event
{
    public bool GateOpened;
}