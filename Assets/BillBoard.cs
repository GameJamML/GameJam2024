using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
        
    void Update()
    {

        Vector3 cameraPosition = Camera.main.transform.position;

        Vector3 lookDirection = cameraPosition - transform.position;

        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

    }
}
