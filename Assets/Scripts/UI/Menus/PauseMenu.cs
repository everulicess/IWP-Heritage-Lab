using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Buttons References")]
    [SerializeField] Button resumeButton;
    [SerializeField] Button controlsButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button quitGameButton;
    [Header("Controls Panel")]
    [SerializeField] GameObject controlsPanel;
    [SerializeField] Button closeControlsButton;
    private void OnEnable()
    {
        controlsPanel.SetActive(false);
    }
    private void Start()
    {
        if (resumeButton == null)
            Debug.LogError($"{nameof(resumeButton)} has not been assigned to {this.gameObject.name}");
        else
            resumeButton.onClick.AddListener(ResumeGame);

        if (controlsButton == null)
            Debug.LogError($"{nameof(controlsButton)} has not been asssigned to {this.gameObject.name}");
        else
            controlsButton.onClick.AddListener(ToggleControls);

        if (mainMenuButton == null)
            Debug.LogError($"{nameof(mainMenuButton)} has not been assigned to {this.gameObject.name}.");
        else
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        if (quitGameButton == null)
            Debug.LogError($"{nameof(quitGameButton)} has not been assigned to {this.gameObject.name}.");
        else
            quitGameButton.onClick.AddListener(QuitGame);

        if (closeControlsButton == null)
            Debug.LogError($"{nameof(closeControlsButton)} has not been assigned to {this.gameObject.name}.");
        else
            closeControlsButton.onClick.AddListener(ToggleControls);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
    // Reload page to effectively "quit"
    Application.ExternalEval("location.reload();");
#else
    Application.Quit();
#endif
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    private void ToggleControls()
    {
        controlsPanel.SetActive(!controlsPanel.activeInHierarchy);
    }

    private void ResumeGame()
    {
        GameManager.Instance.TogglePause();
    }
}
