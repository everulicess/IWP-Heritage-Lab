using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightController : PlayerTools
{
    private new Light light;
    private bool isOn = false;

    private void Awake()
    {
        toolID = PlayerToolsID.Flashlight;
        light = GetComponent<Light>();
        light.enabled = isOn;
        InputManager.Instance.Player.Flashlight.performed += ctx => EventsManager.Broadcast(new OnToolSelected { selectedTool = this.toolID });
    }
    private void Start()
    {
        EventsManager.Broadcast(new OnToolSelected { selectedTool = this.toolID }); //makes this the first tool to have

    }
    private void OnDestroy()
    {
        InputManager.Instance.Player.Flashlight.performed -= ctx => EventsManager.Broadcast(new OnToolSelected { selectedTool = this.toolID });

    }

    protected override void Toggle()
    {
        if (!CanUseTool())
        {
            isOn = false;
            light.enabled = isOn;
            return;
        }
        base.Toggle();
        isOn = !isOn;
        light.enabled = isOn;
    }
}
