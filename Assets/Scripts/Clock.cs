using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Start is called before the first frame update
    int spriteNumber;
    public Renderer _Color;
    public Material[] materials;

    private float sec = 0;

    private float maxsec = 60;

    void Start()
    {
        spriteNumber = 0;
        _Color.material = materials[0];
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;

        if (sec >= maxsec)
        {
            SwapSpriteClock();
            AudioManager.Instace.PlayerSFX(AudioType.Clock);
            sec = 0f;
        }
    }

    void SwapSpriteClock()
    {
        spriteNumber++;
        switch (spriteNumber)
        {
            case 1:
                _Color.material = materials[1];
                break;
            case 2:
                _Color.material = materials[2];
                break;
            case 3:
                _Color.material = materials[3];
                break;
            case 4:
                _Color.material = materials[4];
                break;
            default:
                break;
        }
    }

}
