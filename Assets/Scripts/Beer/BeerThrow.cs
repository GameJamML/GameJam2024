using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerThrow : MonoBehaviour
{

    Vector3 lookDir = Vector3.zero;
    public float throwForce = 10.0f;
    float rotationSpeed = 1.5f;
    public void GatherInfo(Vector3 dir)
    {
        lookDir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(lookDir * throwForce * Time.deltaTime,Space.World);
        transform.RotateAround(transform.position, Vector3.right, 720 * Time.deltaTime);
    }
}
