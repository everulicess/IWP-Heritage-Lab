using System.Collections;
using UnityEngine;

public class StatuePuzzlePiece : PuzzlePiece
{
    [Tooltip("whats the object it should look at")]
    [SerializeField] Transform target;

    [Tooltip("How many degrees off is still considered 'correct'.")]
    [SerializeField, Range(1f, 20f)] float angleTolerance = 10f;

    [Tooltip("Degrees rotated per interaction.")]
    [SerializeField] float rotationStep = 45f;

    [Header("Audio")]
    [Space]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] rotatingSounds;
    bool isRotating = false;
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
    }
    protected override void Interact()
    {
        if (!CanInteract) return;
        if (isRotating) return;
        isRotating = true;

        AudioClip clip = rotatingSounds[Random.Range(0, rotatingSounds.Length)];
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(RotateForClipDuration(rotationStep));
    }
    IEnumerator RotateForClipDuration(float targetAngle)
    {
        float elapsed = 0f;
        float duration = audioSource.clip.length;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, targetAngle, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, endRot, elapsed / duration);
            yield return null;
        }

        transform.rotation = endRot;
        isRotating = false;
        NotifyStateChanged();

    }
}
