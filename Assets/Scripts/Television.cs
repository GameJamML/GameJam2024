using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Television : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyGenerator spawner;
    private bool canStopped = false;
    private bool stopped = false;
    public VideoPlayer teleVideo;
    private Light teleLight;
    private float lightIntensity;


    private float normalTime = 0;
    [SerializeField] private float MaxTimer;
    [SerializeField] private TextMeshProUGUI _canInteractText;
    void Start()
    {
        teleLight = gameObject.GetComponentInChildren<Light>();
        lightIntensity = teleLight.intensity;
        //teleVideo.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canStopped == true)
        {
            spawner.stop = true;
            teleVideo.Stop();
            teleLight.intensity = 0;
            stopped = true;
        }
        else if (stopped == true)
        {
            StopTelevision();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _canInteractText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && spawner.stop == false)
        {
            canStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canStopped = false;
            _canInteractText.gameObject.SetActive(false);
        }
    }

    void StopTelevision()
    {
        canStopped = false;
        normalTime += Time.deltaTime;
        if (normalTime >= MaxTimer)
        {
            stopped = false;
            canStopped = false;
            spawner.stop = false;
            teleVideo.Play();
            teleLight.intensity = lightIntensity;
            normalTime = 0f;
        }
    }
}
