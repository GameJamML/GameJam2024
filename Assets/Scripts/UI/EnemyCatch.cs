using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCatch : MonoBehaviour
{
    private List<KeyCode> _catchKeyList = new();
    [SerializeField]private Image[] _keyImages;
    private bool _startCatching;
    private int _catchIndex = 0;
    private int _difficult = 4;

    public Action<bool> endToCatch;

    private void Start()
    {
        _keyImages = GetComponentsInChildren<Image>();

        foreach (var item in _keyImages)
        {
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TryToCatch();

        if (Input.GetKeyDown(KeyCode.P))
            StartToCatch();
    }

    public void StartToCatch()
    {
        if (_startCatching)
            return;

        _startCatching = true;

        _catchIndex = 0;

        Dictionary<KeyCode, Color> keyPool = new()
        {
            { KeyCode.A, Color.red },
            { KeyCode.S, Color.green },
            { KeyCode.D, Color.blue },
            { KeyCode.W, Color.white },
        };

        for (int i = 0; i < _difficult; i++)
        {
            _catchKeyList.Add(keyPool.OrderBy(x => UnityEngine.Random.value).First().Key);
            _keyImages[i].gameObject.SetActive(true);
            _keyImages[i].color = keyPool[_catchKeyList[i]];
        }
    }

    private void TryToCatch()
    {
        if (!_startCatching)
            return;

        if (!Input.anyKeyDown || Input.GetMouseButtonDown(0))
            return;

        // if press wrong key
        if (!Input.GetKeyDown(_catchKeyList[_catchIndex]))
        {
            _catchKeyList.Clear();
            _startCatching = false;
            // catch fail
            //endToCatch.Invoke(false);
        }
        else
        {
            _keyImages[_catchIndex++].gameObject.SetActive(false);

            if (_catchIndex >= _difficult)
            {
                _catchIndex = 0;
                _startCatching = false;
                _catchKeyList.Clear();
                // catch succesful
                //endToCatch.Invoke(true);
            }
        }

    }
}
