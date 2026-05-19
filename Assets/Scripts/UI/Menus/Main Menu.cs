using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Reference Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button controlsButton;
    [SerializeField] Button exitButton;
    [Header("Controls")]
    [SerializeField] GameObject controlsPanel;
    [SerializeField] Button CloseControlsPanelButton;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startButton == null)
            Debug.LogError($"Start Button has not been assigned to {this.gameObject}");
        else
            startButton.onClick.AddListener(StartGame);

        if (controlsButton == null)
            Debug.LogError($"Controls Button has not been assigned to {this.gameObject}");
        else
            controlsButton.onClick.AddListener(ToggleControls);

        if (exitButton == null)
            Debug.LogError($"Exit Button has not been assigned to {this.gameObject}");
        else
            exitButton.onClick.AddListener(ExitGame);

        if (controlsPanel == null)
            Debug.LogError($"Controls Panel has not been assigned to {this.gameObject}");
        else
            controlsPanel.SetActive(false);

        if (CloseControlsPanelButton == null)
            Debug.LogError($"Close Controls Button has not been assigned to {this.gameObject}");
        else
            CloseControlsPanelButton.onClick.AddListener(ToggleControls);

    }
    void StartGame()
    {
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
    }
    void ToggleControls()
    {
        controlsPanel.SetActive(!controlsPanel.activeInHierarchy);
    }
    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
# endif
    }
   
}
