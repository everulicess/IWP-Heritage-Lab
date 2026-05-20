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

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactableDistance, _interactableMask))
            hitInfo.collider.TryGetComponent(out hit);

        if (hit == _currentTarget) return;

        _currentTarget = hit;

        //if (hit != null)
            EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = hit != null && !isExamining });
        //else
            //EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = false});
    }

    void TryInteract()
    {
        _currentTarget?.Interact();

    }
}
