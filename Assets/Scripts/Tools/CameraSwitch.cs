using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _camera1;
    [SerializeField] private GameObject _camera2;

    private void Start()
    {
        int camera = PlayerPrefs.GetInt("Camera", 2);

        if (camera == 1)
        {
            _camera1.SetActive(true);
            _camera1.tag = "MainCamera";
            _camera2.SetActive(false);
        }
        else
        {
            _camera2.SetActive(true);
            _camera2.tag = "MainCamera";
            _camera1.SetActive(false);
        }
    }
}
