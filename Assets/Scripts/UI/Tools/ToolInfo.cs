using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public class ToolInfo : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Image toolBackground;
    [SerializeField] Image ToolImageHolder;
    [SerializeField] TextMeshProUGUI UITextHolder;

    [Space]
    [Header("Tool Slot States")]
    [SerializeField] Sprite activeTool;
    [SerializeField] Sprite inactiveTool;

    [Space]
    [Header("Tool UI Settings")]
    public PlayerToolsID toolID;
    [SerializeField] Sprite toolSprite;
    [SerializeField] InputActionReference selectToolInput;
    [SerializeField] InputActionReference useToolInput;

    private void Start()
    {
        EventsManager.AddListener<OnToolSelected>(UpdateUI);

        InitializeToolUI();
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnToolSelected>(UpdateUI);

    }
    public void InitializeToolUI()
    {

        if (toolBackground == null)
            Debug.LogError("MISSING TOOL UI PANEL REFERENCE IN INSPECTOR");

        if (ToolImageHolder == null)
            Debug.LogError("MISSING IMAGE HOLDER REFERENCE IN INSPECTOR");
        else
            ToolImageHolder.sprite = toolSprite;

        if (UITextHolder == null)
            Debug.LogError("MISSING TEXT MESH HOLDER REFERENCE IN INSPECTOR");
        else
            UITextHolder.text = $"[{selectToolInput.action.GetBindingDisplayString()}]";
    }

     void ActivateTool(bool activate = true)
    {
        toolBackground.sprite = activate ? activeTool : inactiveTool;
    }
    private void UpdateUI(OnToolSelected evt)
    {
        ActivateTool(toolID == evt.selectedTool);
    }

}
