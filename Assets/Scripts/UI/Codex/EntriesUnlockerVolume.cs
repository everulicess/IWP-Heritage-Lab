using UnityEngine;

public class EntriesUnlockerVolume : MonoBehaviour
{
    InteractableObject interactableObject;
    private void Start()
    {
        interactableObject = GetComponent<InteractableObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        interactableObject.Interact();
    }
}
