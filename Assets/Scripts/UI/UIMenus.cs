using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIMenus : MonoBehaviour
{

    GameState state;

    [Header("Winning")]
    [SerializeField] GameObject winningScreen;
    [SerializeField] GameObject gateScreen;
    [SerializeField] TextMeshProUGUI gateTextHolder;
    private void OnEnable()
    {
        EventsManager.AddListener<OnGateInteraction>(OnFinishedGameInteraction);

        winningScreen.SetActive(false);
        gateScreen.SetActive(false);

    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnGateInteraction>(OnFinishedGameInteraction);
    }

    private void OnFinishedGameInteraction(OnGateInteraction evt)
    {
        if (evt.GateOpened)
        {
            winningScreen.SetActive(true);
        }
        else
        {
            StartCoroutine(nameof(GateTextShowing));
        }
    }
    IEnumerator GateTextShowing()
    {
        gateScreen.SetActive(true);
        gateTextHolder.text = "Puzzles not finished yet! ";
        yield return new WaitForSeconds(2.0f);
        gateScreen.SetActive(false);
    }
}
