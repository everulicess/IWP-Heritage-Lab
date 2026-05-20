using System;
using UnityEngine;

public class StartingGate : MonoBehaviour
{
    bool gateOpened;
    [SerializeField] GameObject[] openedGates;
    [SerializeField] GameObject[] closedGates;
    private void OnEnable()
    {
        EventsManager.AddListener<OnGameStateChanged>(OnGameStateChanged);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnGameStateChanged>(OnGameStateChanged);
    }
  
    void OnGameStateChanged(OnGameStateChanged evt)
    {
        gateOpened = evt.NewState == GameState.Finished;
        if (gateOpened)
            OpenGate();
    }

    private void OpenGate()
    {
        foreach (GameObject go in openedGates)
            go.SetActive(true);

        foreach (GameObject go in closedGates)
            go.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        EventsManager.Broadcast(new OnGateInteraction{ GateOpened = gateOpened });
    }

}
