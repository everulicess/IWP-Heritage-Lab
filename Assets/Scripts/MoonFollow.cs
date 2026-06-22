using UnityEngine;

public class FollowCameraSky : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 initialOffset;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        initialOffset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        transform.position = cameraTransform.position + initialOffset;
        transform.LookAt(cameraTransform.position);
        transform.Rotate(0, 180, 0);
    }
}