using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionBook : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _canInteractText;
    [SerializeField] private RawImage _image;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteractText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_image.gameObject.activeSelf)
                {
                    _image.gameObject.SetActive(true);
                }
                else
                {
                    _image.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _image.gameObject.SetActive(false);
            _canInteractText.gameObject.SetActive(false);
        }
    }
}
