using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerThrow : MonoBehaviour
{

    Vector3 lookDir = Vector3.zero;
    float throwForce = 10.0f;
    public void GatherInfo(Vector3 dir)
    {
        lookDir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(lookDir * throwForce * Time.deltaTime);
    }
}
