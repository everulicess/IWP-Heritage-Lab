using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    [Space]
    [Header("Prefab References")]
    [SerializeField] Image imageHolder;
    [SerializeField] TextMeshProUGUI objectNameHolder;
    [SerializeField] TextMeshProUGUI objectDescriptionHolder;

    private void Start()
    {
        if (imageHolder == null) Debug.LogError($"Reference Missing, {nameof(imageHolder)} in the {gameObject.name} prefab");
        if (objectNameHolder == null) Debug.LogError($"Reference Missing, {nameof(objectNameHolder)} in the {gameObject.name} prefab");
        if (objectDescriptionHolder == null) Debug.LogError($"Reference Missing, {nameof(objectDescriptionHolder)} in the {gameObject.name} prefab");
    }
    public void SetInformation(CodexEntry entry)
    {
        imageHolder.sprite = entry.illustration;
        objectNameHolder.text = entry.name;
        objectDescriptionHolder.text = entry.description;
    } 
}
