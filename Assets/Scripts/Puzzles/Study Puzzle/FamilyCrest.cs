using System;
using UnityEngine;
[RequireComponent(typeof(InteractableObject))]
public class FamilyCrest : MonoBehaviour
{
    [SerializeField] CrestID crestID;
    InteractableObject interactable;
    private void OnEnable()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteraction.AddListener(Interact);
    }

    private void Interact()
    {
        EventsManager.Broadcast(new CarriedCrest { carriedID = crestID });
    }
}
