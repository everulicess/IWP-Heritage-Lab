using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EMFDetectable : MonoBehaviour
{
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
        if (Vector3.Distance(this.gameObject.transform.position, evt.EMFPosition) <= evt.EMFDetectionDistance)
        {
            Debug.Log($"EMF ha Detected this opbjec:{this.gameObject.name}");
            StartCoroutine(ReactionSequence());
        }
    }

    IEnumerator ReactionSequence()
    {
        GameObject spawn = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), this.transform);
        yield return new WaitForSeconds(2.0f);
        Destroy(spawn);
    }
}
