using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState currentState { get; private set; }
    GameState previousState;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetState(GameState.Gameplay);
    }

    private void OnEnable()
    {
        EventsManager.AddListener<OnCodexOpened>(OnCodexOpened);
        EventsManager.AddListener<OnCodexClosed>(OnCodexClosed);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnCodexOpened>(OnCodexOpened);
        EventsManager.RemoveListener<OnCodexClosed>(OnCodexClosed);
    }
    public void SetState(GameState newState)
    {
        previousState = currentState;
        Debug.Log($"CURRENT GAME STATE {newState}");
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
    }
}
