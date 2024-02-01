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

        if (_flashing)
            return;

        StopCoroutine(ChangeColorAnim());

        StartCoroutine(ChangeColorAnim());
    }

    public void StartFlashEvent(Color color)
    {
        _flashing = true;

        StartCoroutine(FlashinAnim(color));
    }

    public void CloseFlashEvent()
    {
        _flashing = false;
    }

    private IEnumerator FlashinAnim(Color color)
    {
        float t = 0;

        bool to = true;

        Color beginColor = Color.black;
        beginColor.a = 0;

        while (_flashing)
        {
            if (to)
            {
                t += Time.deltaTime;

                if (t >= 1)
                    to = false;
            }
            else
            {
                t -= Time.deltaTime;

                if (t <= 0)
                    to = true;
            }

            _image.color = Color.Lerp(beginColor, color, t);

            yield return null;
        }

        color = _image.color;

        for (t = 0.0f; t <= 1; t += Time.deltaTime)
        {
            _image.color = Color.Lerp(color, beginColor, t);

            yield return null;
        }
    }

    private IEnumerator ChangeColorAnim()
    {
        Color beginCol = _image.color;

        for (float t = 0; t < 1; t += Time.deltaTime * transitionSpeed)
        {
            _image.color = Color.Lerp(beginCol, _destinationColor, t);

            yield return null;
        }
    }
}
