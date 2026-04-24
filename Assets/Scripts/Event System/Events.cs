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
#region CODEX EVENTS 
//CODEX EVENTS
public class UnlockEntry : Event
{
    public CodexEntry Entry;
}
#endregion
