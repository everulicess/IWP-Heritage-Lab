using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] float _interactableDistance = 3.0f;

    [SerializeField] Transform _cameraTransform;
    InteractableObject _currentTarget;
    bool isExamining = false;

    public void Init(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }

    private void OnEnable()
    {
        InputManager.Instance.Player.Interact.performed += ctx => TryInteract();

        EventsManager.AddListener<OnExamineObject>(SetExamineStatus);
    }

    private void OnDisable()
    {
        InputManager.Instance.Player.Interact.performed -= ctx => TryInteract();

        EventsManager.RemoveListener<OnExamineObject>(SetExamineStatus);

    }

    private void SetExamineStatus(OnExamineObject evt)
    {
        isExamining = evt.StartExamination;
        EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = isExamining == false });
        _currentTarget = null;

        Debug.Log($"OBJECT BEING EXAMIINED = {isExamining}");
    }

    void Update()
    {
        Ray ray = new(_cameraTransform.position, _cameraTransform.forward);
        InteractableObject hit = null;
        EntriesUnlockerVolume entries = null;
        Puzzle puzzle = null;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactableDistance, _interactableMask))
        {
            hitInfo.collider.TryGetComponent(out hit);
            hitInfo.collider.TryGetComponent(out entries);
            hitInfo.collider.TryGetComponent(out puzzle);
        }

        if (entries != null) return;
        if (puzzle != null) return;
        if (hit == _currentTarget) return;

        _currentTarget = hit;

            EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = hit != null && !isExamining });
    }

    void TryInteract()
    {
        _currentTarget?.Interact();

    }
}
