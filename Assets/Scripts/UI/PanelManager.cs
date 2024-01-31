using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Tooltip("Time in second")]
    public float transitionSpeed = 1.0f;

    private Image _image;

    private Color _destinationColor;

    private bool _flashing = false;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeColor(Color color)
    {
        _destinationColor = color;

        StopCoroutine(ChangeColorAnim());

        StartCoroutine(ChangeColorAnim());
    }

    private IEnumerator FlashinAnim()
    {
        yield return null;
    }

    private IEnumerator ChangeColorAnim()
    {
        Color beginCol = _image.color;

        for (float t = 0; t < 1; t += Time.deltaTime * transitionSpeed)
        {
            _image.color = Color.Lerp(beginCol, _destinationColor,t);

            yield return null;
        }       
    }
}
