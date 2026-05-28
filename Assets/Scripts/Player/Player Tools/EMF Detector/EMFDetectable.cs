using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EMFDetectable : MonoBehaviour
{
    [SerializeField] float reactionDuration = 2.0f;
    [SerializeField] ParticleSystem particles;
    Coroutine activeReaction;
    private void OnEnable()
    {
        EventsManager.AddListener<OnEMFDetection>(ReactToEMF);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnEMFDetection>(ReactToEMF);

    }

    private void ReactToEMF(OnEMFDetection evt)
    {
        if (Vector3.Distance(this.gameObject.transform.position, evt.EMFPosition) > evt.EMFDetectionDistance)
            return;
            Debug.Log($"EMF ha Detected this opbjec:{this.gameObject.name}");
        if (activeReaction != null)
            StopCoroutine(activeReaction);
        activeReaction = StartCoroutine(ReactionSequence());

    }

    IEnumerator ReactionSequence()
    {
        particles.Play();
        yield return new WaitForSeconds(reactionDuration);
        particles.Stop();
        activeReaction = null;
    }
}
