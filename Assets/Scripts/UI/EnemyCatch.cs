using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCatch : MonoBehaviour
{
    private class KeyToPress
    {
        public KeyToPress(Image img)
        {
            image = img;
        }

        public void AddKey(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.UpArrow:
                    alternativeKey = KeyCode.W;
                    break;
                case KeyCode.DownArrow:
                    alternativeKey = KeyCode.S;
                    break;
                case KeyCode.LeftArrow:
                    alternativeKey = KeyCode.A;
                    break;
                case KeyCode.RightArrow:
                    alternativeKey = KeyCode.D;
                    break;
                default:
                    return;
            }

            this.key = key;
        }

        public KeyCode key = KeyCode.None;
        public KeyCode alternativeKey;
        public Image image;
    }

    // Resources of sprites
    [SerializeField]
    private List<KeySpritePair> _spriteList;
    [Serializable]
    private class KeySpritePair
    {
        public KeyCode key;
        public Sprite value;
    }

    private List<KeyToPress> _keyToPressList = new();

    [SerializeField, Header("Only for read")] private bool _startCatching = false;
    [SerializeField, Header("Only for read")] private int _catchIndex = 1;
    private int _difficult = 4;

    public Action<bool> endToCatch;

    private RectTransform _myRecTransform;

    private void Start()
    {
        _myRecTransform = GetComponent<RectTransform>();
        Image[] _keyImages = GetComponentsInChildren<Image>();

        foreach (var img in _keyImages)
        {
            img.gameObject.SetActive(false);

            KeyToPress kp = new(img);

            _keyToPressList.Add(kp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TryToCatch();
    }

    public void StartToCatch()
    {
        if (_startCatching)
            return;

        if (_catchIndex != 0)
        {
            _catchIndex = 0;

            // Init keys
            for (int i = 0; i < _difficult; i++)
            {
                KeySpritePair kp = _spriteList[UnityEngine.Random.Range(0, _spriteList.Count)];

                _keyToPressList[i].AddKey(kp.key);
                _keyToPressList[i].image.sprite = kp.value;
                _keyToPressList[i].image.gameObject.SetActive(true);
            }

            StartCoroutine(StartToCatchAnim());
        }
    }

    private IEnumerator StartToCatchAnim()
    {
        Vector2 beginPos = new(0, -30);
        Vector2 endPos = new(0, 0);

        Color beginColor = Color.white;
        Color endColor = Color.white;

        beginColor.a = 0;

        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            for (int i = 0; i < _difficult; i++)
            {
                _keyToPressList[i].image.color = Color.Lerp(beginColor, endColor, t);
            }

            _myRecTransform.localPosition = Vector2.Lerp(beginPos, endPos, t);

            yield return null;
        }

        _startCatching = true;
    }

    private void TryToCatch()
    {
        if (!_startCatching)
            return;

        if (!Input.anyKeyDown || Input.GetMouseButtonDown(0))
            return;

        // if press wrong key
        if (!Input.GetKeyDown(_keyToPressList[_catchIndex].key) && !Input.GetKeyDown(_keyToPressList[_catchIndex].alternativeKey))
        {
            CatchFaild();
        }
        else
        {
            _keyToPressList[_catchIndex++].image.gameObject.SetActive(false);

            // when finishd catch
            if (_catchIndex >= _difficult)
            {
                _startCatching = false;
                _catchIndex = -1;

                // Close All images
                for (int i = 0; i < _difficult; i++)
                {
                    _keyToPressList[i].image.gameObject.SetActive(false);
                }

                // catch succesful
                endToCatch?.Invoke(true);
            }
        }
    }

    public void CatchFaild()
    {
        for (int i = 0; i < _difficult; i++)
        {
            _keyToPressList[i].image.gameObject.SetActive(false);
        }

        _startCatching = false;
        _catchIndex = -1;

        // catch fail
        endToCatch?.Invoke(false);
    }
}
