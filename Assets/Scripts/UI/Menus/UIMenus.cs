using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIMenus : MonoBehaviour
{
    [Header("Winning")]
    [SerializeField] GameObject winningScreen;
    [SerializeField] GameObject gateScreen;
    [SerializeField] TextMeshProUGUI gateTextHolder;
    [Space]
    [Header("Pause")]
    [SerializeField] GameObject pauseOverlay;
    [Space]
    [Header("Interaction Text")]
    [SerializeField] GameObject interactionText;
    private void OnEnable()
    {
        EventsManager.AddListener<OnGateInteraction>(OnFinishedGameInteraction);
        EventsManager.AddListener<OnGameStateChanged>(OnGameStateChanged);
        EventsManager.AddListener<OnInteractionPrompt>(InteractionPrompt);

        winningScreen.SetActive(false);
        gateScreen.SetActive(false);
        interactionText.SetActive(false);
    }

    private void InteractionPrompt(OnInteractionPrompt evt)
    {
        interactionText.SetActive(evt.ShowPrompt);
    }

    private void OnDisable()
    {
        EventsManager.RemoveListener<OnGateInteraction>(OnFinishedGameInteraction);
        EventsManager.RemoveListener<OnGameStateChanged>(OnGameStateChanged);
        EventsManager.RemoveListener<OnInteractionPrompt>(InteractionPrompt);

    }
    private void OnFinishedGameInteraction(OnGateInteraction evt)
    {
        if (evt.GateOpened)
            winningScreen.SetActive(true);
        else
            StartCoroutine(nameof(GateTextShowing));
    }
    IEnumerator GateTextShowing()
    {
        gateScreen.SetActive(true);
        gateTextHolder.text = "Puzzles not finished yet! ";
        yield return new WaitForSeconds(2.0f);
        gateScreen.SetActive(false);
    }
    void OnGameStateChanged(OnGameStateChanged evt)
    {
        pauseOverlay.SetActive(evt.NewState == GameState.Paused);
    }
}
