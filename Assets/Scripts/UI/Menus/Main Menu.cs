using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Reference Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button controlsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button CreditsButton;
    [Space]
    [Header("Controls")]
    [SerializeField] GameObject controlsPanel;
    [SerializeField] Button CloseControlsPanelButton;
    [Space]
    [Header("Credits")]
    [SerializeField] GameObject creditsPanel;
    [SerializeField] Button CloseCreditsPanelButton;
    


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

        if (creditsPanel == null)
            Debug.LogError($"Credits Panel has not been assigned to {this.gameObject}");
        else
            creditsPanel.SetActive(false);

        if (CloseCreditsPanelButton == null)
            Debug.LogError($"Close Controls Button has not been assigned to {this.gameObject}");
        else
            CloseCreditsPanelButton.onClick.AddListener(ToggleCredits);

        if (CreditsButton == null)
            Debug.LogError($"Controls Button has not been assigned to {this.gameObject}");
        else
            CreditsButton.onClick.AddListener(ToggleCredits);
    }

    private void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
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
#elif UNITY_WEBGL
    // Reload page to effectively "quit"
    Application.ExternalEval("location.reload();");
#else
    Application.Quit();
#endif
    }

}
