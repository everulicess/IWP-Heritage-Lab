using UnityEngine;

public class EMFDetectorController : PlayerTools
{
    [SerializeField] float detectionDistance = 10.0f;
    private void Awake()
    {
        toolID = PlayerToolsID.EMFDetector;
        InputManager.Instance.Player.EMFDetector.performed += ctx => EventsManager.Broadcast(new OnToolSelected { selectedTool = this.toolID });
    }
    private void OnDestroy()
    {
        InputManager.Instance.Player.EMFDetector.performed -= ctx => EventsManager.Broadcast(new OnToolSelected { selectedTool = this.toolID });

    }
    protected override void Toggle()
    {
        if (!CanUseTool())
            return;
        base.Toggle();
        EventsManager.Broadcast(new OnEMFDetection { EMFDetectionDistance = detectionDistance, EMFPosition = this.gameObject.transform.position });
    }
}
