using UnityEngine;

public class PulsationForceField : MonoBehaviour
{
    public Material targetMaterial; 
    public float minTransparency = 0.25f;
    public float maxTransparency = 0.5f;
    public float pulsationSpeed = 1.0f;

    void Start()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("Target Material not assigned!");
            return;
        }
    }

    void Update()
    {
        float transparency = Mathf.Lerp(minTransparency, maxTransparency, Mathf.PingPong(Time.time * pulsationSpeed, 1.0f));
        UpdateMaterialTransparency(transparency);
    }

    void UpdateMaterialTransparency(float transparency)
    {
        targetMaterial.SetFloat("_Transparency", transparency);
    }
}