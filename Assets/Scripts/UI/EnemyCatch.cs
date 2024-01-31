using System;
using System.Linq;
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

        public bool GetHit()
        {
            if (--life <= 0)
                return true;

            if (getNextSprite != null)
                image.sprite = getNextSprite(key, life - 1);

            return false;
        }

        public KeyCode key = KeyCode.None;
        public KeyCode alternativeKey;
        public int life = 1;
        public Image image;
        public Func<KeyCode, int, Sprite> getNextSprite;
    }

    // Resources of sprites
    [SerializeField]
    private List<KeySpritePair> _spriteList;
    [Serializable]
    private class KeySpritePair
    {
        public KeyCode key;
        public Sprite[] value;
    }

    private List<KeyToPress> _keyToPressList = new();

    [SerializeField, Header("Only for read")] private bool _startCatching = false;
    [SerializeField, Header("Only for read")] private int _catchIndex = 1;
    private int _difficult = 5;

    public Action<bool> endToCatch;

    private RectTransform _myRecTransform;

    [SerializeField] private PanelManager _panelManager;

    private void Start()
    {
        if (_panelManager == null)
            _panelManager = FindAnyObjectByType<PanelManager>();

        _myRecTransform = GetComponent<RectTransform>();
        Image[] _keyImages = GetComponentsInChildren<Image>();

        foreach (var img in _keyImages)
        {
            img.gameObject.SetActive(false);

            KeyToPress kp = new(img);

            kp.getNextSprite = (key, value) => _spriteList.FirstOrDefault(item => item.key == key).value[value];

            _keyToPressList.Add(kp);
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _keyToPressList)
        {
            item.getNextSprite = null;
        }

        _keyToPressList.Clear();
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

                int life = UnityEngine.Random.Range(0, 3);

                _keyToPressList[i].image.sprite = kp.value[life];
                _keyToPressList[i].life = life + 1;
                _keyToPressList[i].image.gameObject.SetActive(true);
            }

            _panelManager.ChangeColor(Color.black);

            StartCoroutine(StartToCatchAnim());
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

        _panelManager.ChangeColor(new Color(0, 0, 0, 0));

        // catch fail
        endToCatch?.Invoke(false);
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
            AudioManager.Instace.PlayerSFX(AudioType.TryAgain);
        }
        else
        {
            if (_keyToPressList[_catchIndex].GetHit())
            {
                _keyToPressList[_catchIndex++].image.gameObject.SetActive(false);
            }

            // Audio
            switch (_keyToPressList[_catchIndex].key)
            {
                case KeyCode.LeftAlt:
                    AudioManager.Instace.PlayerSFX(AudioType.Correctkey, 0.5f);
                    break;
                case KeyCode.DownArrow:
                    AudioManager.Instace.PlayerSFX(AudioType.Correctkey, 1f);
                    break;
                case KeyCode.RightArrow:
                    AudioManager.Instace.PlayerSFX(AudioType.Correctkey, 1.5f);
                    break;
                case KeyCode.UpArrow:
                    AudioManager.Instace.PlayerSFX(AudioType.Correctkey, 2f);
                    break;
            }            

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

                _panelManager.ChangeColor(new Color(0, 0, 0, 0));

                // catch succesful
                endToCatch?.Invoke(true);
            }
        }
    }
}
