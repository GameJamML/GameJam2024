using UnityEngine;

public class GhostLight : MonoBehaviour
{

    public Transform center;
    public float rotationSpeed = 50.0f;

    void Update()
    {
        if (center == null)
        {
            Debug.LogError("Center not assigned!");
            return;
        }

        float angle = Time.deltaTime * rotationSpeed;

        transform.RotateAround(center.position, Vector3.up, angle);
    }
}
