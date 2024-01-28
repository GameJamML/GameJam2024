using UnityEngine;
using Cinemachine;

public class VCTrigger : MonoBehaviour
{

    [SerializeField] private Transform _Player;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void Start()
    {
        cam.LookAt = _Player.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.Priority = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.Priority = 0;
        }
    }
}
