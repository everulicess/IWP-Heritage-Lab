using UnityEngine;

public class LightPuzzlePiece : PuzzlePiece
{
    [Header("Light Settings")]
    [SerializeField] Light targetLight;

    //in case we want hte lit cnadle ot have a "flame" model on it we can also enable
    [Tooltip("Optional visual object")]
    [SerializeField] GameObject lightVisual;

    [SerializeField] bool startsOn = true;

    bool isOn;

    public override bool IsInCorrectState
    {
        get
        {
            return !isOn;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        isOn = startsOn;
        ApplyVisualState();
    }

    public override void Interact()
    {
        if (!CanInteract)
        {
            return;
        }

        isOn = !isOn;
        ApplyVisualState();
        NotifyStateChanged();
    }

    void ApplyVisualState()
    {
        targetLight.enabled = isOn;

        if(lightVisual != null)
        {
            lightVisual.SetActive(isOn);
        } 
    }
}