using UnityEngine;
using UnityEngine.UI;

public class CameraChooser : MonoBehaviour
{
    [SerializeField] private Toggle _camera1Toggle;
    [SerializeField] private Toggle _camera2Toggle;

    private void Start()
    {
        int camera = PlayerPrefs.GetInt("Camera", 1);

        if (camera == 1)
        {
            _camera1Toggle.isOn = true;
            _camera2Toggle.isOn = false;
        }
        else
        {
            _camera1Toggle.isOn = false;
            _camera2Toggle.isOn = true;
        }
    }

    public void OnCameraSwitch(int camera)
    {
        if (camera == 1)
        {
            if (!_camera1Toggle.isOn)
                return;

            PlayerPrefs.SetInt("Camera", 1);
        }
        else
        {
            if (!_camera2Toggle.isOn)
                return;

            PlayerPrefs.SetInt("Camera", 2);
        }
    }
}
