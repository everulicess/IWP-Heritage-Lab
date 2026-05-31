using UnityEngine;

public class AppllicationSettings : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
        QualitySettings.vSyncCount = 0; // Must be 0, otherwise targetFrameRate is ignored
        DontDestroyOnLoad(this);
    }
    private float _deltaTime;
    private GUIStyle _style;

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        if (_style == null)
        {
            _style = new GUIStyle(GUI.skin.label);
            _style.fontSize = 24;
            _style.normal.textColor = Color.white;
        }

        float fps = 1.0f / _deltaTime;
        GUI.Label(new Rect(10, 10, 200, 40), $"FPS: {fps:0.}");
    }
}
