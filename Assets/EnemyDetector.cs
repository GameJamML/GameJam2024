using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float maxAimAngle = 40.0f;


    [HideInInspector] public bool _enemyDetected = false;
    private Transform _parentTransform;


    [HideInInspector] public int _targetInstanceID = -1;
    [HideInInspector] public Transform _target = null;
    private float _targetAngle = 360.0f;
    private float _targetDistanceToPlayer = float.PositiveInfinity;
    

    private void Start()
    {
        _parentTransform = gameObject.GetComponentInParent<Transform>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            int id = other.GetInstanceID();
            if (id == _targetInstanceID)
            {
                Vector3 aux = other.transform.position - _parentTransform.position;
                float auxAngle = Vector3.Angle(transform.forward, aux);
                if (auxAngle < maxAimAngle)
                {
                    _targetAngle = auxAngle;
                }
                else OnTriggerExit(other);
                return;
            }

            //Checks if the target is on front by 180 degrees;
            Vector3 directionToTarget = other.transform.position - _parentTransform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (angle < maxAimAngle)
            {
                //Checks which target is closest to the center (forward).
                if (angle < _targetAngle)
                {
                    //Checks which target is closest to the player's position
                    float distanceFromTarget = Vector3.Distance(_parentTransform.position, other.transform.position);
                    if (distanceFromTarget < _targetDistanceToPlayer)
                    {
                        _targetInstanceID = id;
                        _target = other.transform;
                        _targetAngle = angle;
                        _targetDistanceToPlayer = distanceFromTarget;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_targetInstanceID == other.GetInstanceID())
            {
                //Clear target
                _targetInstanceID = -1;
                _target = null;
                _targetAngle = 360.0f;
                _targetDistanceToPlayer = float.PositiveInfinity;
            }
        }
    }
}
