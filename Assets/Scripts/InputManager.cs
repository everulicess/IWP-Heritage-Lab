using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    Player_InputActions _actions;

    public Player_InputActions.PlayerActions Player => _actions.Player;
    public Player_InputActions.UIActions UI => _actions.UI;
    public Player_InputActions.GlobalActions Global => _actions.Global;

    private void OnEnable()
    {
        EventsManager.AddListener<OnGameStateChanged>(OnStateChanged);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnGameStateChanged>(OnStateChanged);
    }
    void OnStateChanged(OnGameStateChanged evt)
    {
        switch (evt.NewState)
        {
            case GameState.Gameplay:
                SwitchToGameplay();
                break;
            case GameState.Codex:
                SwitchToUI();
                break;
            case GameState.Paused:
            case GameState.Cutscene:
            case GameState.Tutorial:
                DisableAll();
                break;
        }
    }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _actions = new Player_InputActions();
        _actions.Global.Enable();
        SwitchToGameplay();

    }

    void OnDestroy() => _actions.Dispose();

    public void SwitchToGameplay()
    {
        _actions.UI.Disable();
        _actions.Player.Enable();
    }

    public void SwitchToUI()
    {
        _actions.Player.Disable();
        _actions.UI.Enable();
    }

    public void DisableAll()
    {
        _actions.Player.Disable();
        _actions.UI.Disable();
    }
}
