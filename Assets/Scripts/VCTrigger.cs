using UnityEngine;
using Cinemachine;

public class VCTrigger : MonoBehaviour
{

    [SerializeField] private Transform _Player;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void Start()
    {
        cam.LookAt = _Player.transform;
        //cam.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //cam.gameObject.SetActive(true);
            cam.Priority = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.Priority = 0;
            //cam.gameObject.SetActive(false);
        }
    }
}
