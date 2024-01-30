using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepsOnWood;
    private PlayerMove playerControler;

    private void Start()
    {
        playerControler = GetComponent<PlayerMove>();
    }

    void PlayFootstepSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = Random.Range(0.9f, 1.0f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);


        //if (footstepsOnWood.Length > 0 && playerControler.canInteract == true)
        if (footstepsOnWood.Length > 0)
        {
            audioSource.PlayOneShot(footstepsOnWood[Random.Range(0, footstepsOnWood.Length)]);
        }
    }
}
