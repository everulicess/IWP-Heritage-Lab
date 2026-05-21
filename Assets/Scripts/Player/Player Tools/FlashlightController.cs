using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light light;
    private bool isOn = false;

    private void Awake()
    {
        light = GetComponent<Light>();
        light.enabled = isOn;
        InputManager.Instance.Player.Flashlight.performed += ctx => Toggle();
    }
    private void OnEnable()
    {
        InputManager.Instance.Player.Flashlight.performed -= ctx => Toggle();

    }
    private void Toggle()
    {
        isOn = !isOn;
        light.enabled = isOn;
    }
}
