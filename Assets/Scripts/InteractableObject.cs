using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent OnInteraction;
    public void Interact()
    {
        OnInteraction.Invoke();
    }
}
