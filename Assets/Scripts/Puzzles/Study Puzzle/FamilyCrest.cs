using System;
using UnityEngine;
[RequireComponent(typeof(InteractableObject))]
public class FamilyCrest : MonoBehaviour
{
    [SerializeField] CrestID crestID;
    InteractableObject interactable;
    [SerializeField]GameObject crest;
    private void OnEnable()
    {
        EventsManager.AddListener<CarriedCrest>(CrestState);
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteraction.AddListener(Interact);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<CarriedCrest>(CrestState);

    }

    private void CrestState(CarriedCrest evt)
    {
        crest.SetActive(crestID != evt.carriedID);
        interactable.canBeInteracted = crestID != evt.carriedID;
        //interactable.enabled = ;
    }

    private void Interact()
    {
        EventsManager.Broadcast(new CarriedCrest { carriedID = crestID });
        EventsManager.Broadcast(new OnInteractionPrompt { ShowPrompt = false });

    }
}
