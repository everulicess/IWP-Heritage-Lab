using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteraction;
    [HideInInspector]public bool canBeInteracted = true;
    private void Start()
    {
        if ( this.gameObject.layer != 6)
            this.gameObject.layer = 6;
    }
    public void Interact()
    {
        OnInteraction.Invoke();
    }
}
