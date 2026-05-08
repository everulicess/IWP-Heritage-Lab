using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteraction;
    public void Interact()
    {
        OnInteraction.Invoke();
    }
}
