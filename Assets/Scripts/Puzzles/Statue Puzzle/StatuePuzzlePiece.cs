using UnityEngine;

public class StatuePuzzlePiece : PuzzlePiece
{
    [Tooltip("whats the object it should look at")]
    [SerializeField] Transform target;

    [Tooltip("How many degrees off is still considered 'correct'.")]
    [SerializeField, Range(1f, 20f)] float angleTolerance = 10f;

    [Tooltip("Degrees rotated per interaction.")]
    [SerializeField] float rotationStep = 45f;
    new Renderer renderer;
    public override bool IsInCorrectState{
        get
        {
        if (target == null) return false;
        Vector3 toTarget = (target.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, toTarget) <= angleTolerance;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponentInChildren<Renderer>();
    }
    public override void Interact()
    {
        if (!CanInteract) return;
        transform.Rotate(Vector3.up, rotationStep, Space.World);

        renderer.material.color = IsInCorrectState ? Color.green : Color.red;
        
        NotifyStateChanged();
    }
}
