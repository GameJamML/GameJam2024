using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Start is called before the first frame update
    int spriteNumber;
    public Renderer _Color;
    public Texture[] textures;
    void Start()
    {
        spriteNumber = 0;
        _Color.material.mainTexture = textures[0];
    }

    private void OnEnable()
    {
        Timer.MinutePassed += SwapSpriteClock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwapSpriteClock()
    {
        spriteNumber++;
        switch (spriteNumber)
        {
            case 1:
                _Color.material.mainTexture = textures[1];
                break;
            case 2:
                _Color.material.mainTexture = textures[2];
                break;
            case 3:
                _Color.material.mainTexture = textures[3];
                break;
            case 4:
                _Color.material.mainTexture = textures[4];
                break;
            default:
                break;
        }
    }

}
