using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class DisableFogForCamera : MonoBehaviour
{
    private Camera targetCamera;
    private bool originalFogState;

    void OnEnable()
    {
        targetCamera = GetComponent<Camera>();
        RenderPipelineManager.beginCameraRendering += OnBeginCamera;
        RenderPipelineManager.endCameraRendering += OnEndCamera;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCamera;
        RenderPipelineManager.endCameraRendering -= OnEndCamera;
    }

    private void OnBeginCamera(ScriptableRenderContext context, Camera camera)
    {
        if (camera == targetCamera)
        {
            originalFogState = RenderSettings.fog;
            RenderSettings.fog = false;
        }
    }

    private void OnEndCamera(ScriptableRenderContext context, Camera camera)
    {
        if (camera == targetCamera)
        {
            RenderSettings.fog = originalFogState; // Put fog back to normal for the rest of the game
        }
    }
}
