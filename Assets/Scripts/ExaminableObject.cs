using System;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ExaminableObject : MonoBehaviour
{
   InteractableObject interactableObject;
    [SerializeField] GameObject objectToExamine;
    [Tooltip("Set the rotation that should be used in the inspecting mode.")]
    [SerializeField] Quaternion objectToExamineRotation;
    private void OnEnable()
    {
        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteraction.AddListener(Interact);
    }
    private void OnDisable()
    {
        interactableObject.OnInteraction.RemoveListener(Interact);
    }

    private void Interact()
    {
        if (objectToExamine == null)
            Debug.Log($"Missing a refernce for {nameof(objectToExamine)}");
        EventsManager.Broadcast(new OnExamineObject { Target = objectToExamine, StartExamination = true, TargetInitialInspectRotation = objectToExamineRotation });
    }
}
