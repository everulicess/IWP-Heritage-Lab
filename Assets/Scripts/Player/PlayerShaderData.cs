using UnityEngine;

public class PlayerShaderData : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}