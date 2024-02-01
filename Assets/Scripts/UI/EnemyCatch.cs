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

    [Range(5, 20), Space] public float _maxCoolDown = 6.0f;
    private float _coolDown = 0;

    [Range(3, 10)] public int _difficult = 3;

    public Action<bool> endToCatch;

    private RectTransform _myRecTransform;

    [SerializeField] private PanelManager _panelManager;

    [SerializeField] private Slider _coolDownSlider;

    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField, Header("Only for read")] private int _catchIndex = 1;

    private float _smallKeyScale = 0.6f;

    private void Start()
    {
        if (_panelManager == null)
            _panelManager = FindAnyObjectByType<PanelManager>();

        if (_coolDownSlider == null)
            _coolDownSlider = GetComponentInParent<Slider>();

        if (_canvasGroup == null)
            _canvasGroup = GetComponentInParent<CanvasGroup>();

        _myRecTransform = GetComponent<RectTransform>();
        Image[] _keyImages = GetComponentsInChildren<Image>();

        foreach (var img in _keyImages)
        {
            img.gameObject.SetActive(false);

            img.rectTransform.localScale = Vector3.one * _smallKeyScale;

            KeyToPress kp = new(img);

            kp.getNextSprite = (key, value) => _spriteList.FirstOrDefault(item => item.key == key).value[value];

            _keyToPressList.Add(kp);
        }

        Timer.MinutePassed += NexLevel;
    }

    private void OnDestroy()
    {
        foreach (var item in _keyToPressList)
        {
            item.getNextSprite = null;
        }

        _keyToPressList.Clear();

        Timer.MinutePassed -= NexLevel;
    }

    // Update is called once per frame
    void Update()
    {
        TryToCatch();
    }

    private void NexLevel()
    {
        _maxCoolDown -= 0.2f;

        _difficult++;
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

            _keyToPressList[0].image.rectTransform.localScale = Vector3.one;

            _coolDown = _maxCoolDown;
            _coolDownSlider.value = 1;

            _panelManager.ChangeColor(Color.black);

            StopCoroutine(EndToCatchAnim());
            StartCoroutine(StartToCatchAnim());
        }
    }

    public void CatchFaild()
    {
        AudioManager.Instace.PlayerSFX(AudioType.TryAgain);

        for (int i = 0; i < _difficult; i++)
        {
            _keyToPressList[i].image.rectTransform.localScale = Vector3.one * _smallKeyScale;
            _keyToPressList[i].image.gameObject.SetActive(false);
        }

        _startCatching = false;
        _catchIndex = -1;

        _panelManager.ChangeColor(new Color(0, 0, 0, 0));

        StopCoroutine(StartToCatchAnim());
        StartCoroutine(EndToCatchAnim());

        // catch fail
        endToCatch?.Invoke(false);
    }

    private IEnumerator StartToCatchAnim()
    {
        /*
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
        */

        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            _canvasGroup.alpha = t;

            yield return null;
        }

        _canvasGroup.alpha = 1;

        _startCatching = true;
    }

    private IEnumerator EndToCatchAnim()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime * 2)
        {
            _canvasGroup.alpha = t;

            yield return null;
        }

        _canvasGroup.alpha = 0;
    }

    private void TryToCatch()
    {
        if (!_startCatching)
            return;

        _coolDown -= Time.deltaTime;
        _coolDownSlider.value = _coolDown / _maxCoolDown;

        if (_coolDown <= 0)
        {
            CatchFaild();
            return;
        }

        if (!Input.anyKeyDown || Input.GetMouseButtonDown(0))
            return;

        if (_keyToPressList.Count <= _catchIndex || _catchIndex < 0)
            return;

        // if press wrong key
        if (!Input.GetKeyDown(_keyToPressList[_catchIndex].key) && !Input.GetKeyDown(_keyToPressList[_catchIndex].alternativeKey))
        {
            CatchFaild();
        }
        else
        {
            if (_keyToPressList[_catchIndex].GetHit())
            {
                _keyToPressList[_catchIndex].image.rectTransform.localScale = Vector3.one * _smallKeyScale;
                _keyToPressList[_catchIndex++].image.gameObject.SetActive(false);
                _keyToPressList[_catchIndex].image.rectTransform.localScale = Vector3.one;
            }

            // Audio
            switch (_keyToPressList[_catchIndex].key)
            {
                case KeyCode.LeftArrow:
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

                StartCoroutine(EndToCatchAnim());

                // catch succesful
                endToCatch?.Invoke(true);
            }
        }
    }
}
