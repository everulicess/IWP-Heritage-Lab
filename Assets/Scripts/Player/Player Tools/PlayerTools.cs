using Unity.VisualScripting;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    PlayerToolsID currenTool;
    [HideInInspector]public PlayerToolsID toolID;

    [Space]
    [Header("Audio")]
    AudioSource source;
    [SerializeField] AudioClip toolSound;
    protected virtual void OnEnable()
    {
        InputManager.Instance.Player.UseTool.performed += ctx => Toggle();
        EventsManager.AddListener<OnToolSelected>(ChangeTool);
        source = GetComponent<AudioSource>();
        if (source == null )
        { 
            source = this.AddComponent<AudioSource>(); 
            source.clip = toolSound;
        }
    }
    protected virtual void OnDisable()
    {
        InputManager.Instance.Player.UseTool.performed -= ctx => Toggle();
        EventsManager.RemoveListener<OnToolSelected>(ChangeTool);

    }
    protected virtual void Toggle()
    {
        Debug.Log($"USING ONE OF THE TOOLS {this.name}");
        source.Play();
    }

    protected bool CanUseTool()
    {
        return currenTool == toolID;
    }
    private void ChangeTool(OnToolSelected evt)
    {
        currenTool = evt.selectedTool;
        if ( currenTool != toolID)
            Toggle();
    }
}
