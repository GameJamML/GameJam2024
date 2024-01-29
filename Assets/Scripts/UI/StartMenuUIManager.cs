using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUIManager : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    [Header("Menu")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;

    private void Awake()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        startButton.interactable = true;
        optionsButton.interactable = true;
        creditsButton.interactable = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(optionsMenu.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                optionsMenu.SetActive(false);
                startButton.interactable = true;
                optionsButton.interactable = true;
                creditsButton.interactable = true;
                exitButton.interactable = true;
            }
        }

        if (creditsMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                creditsMenu.SetActive(false);
                startButton.interactable = true;
                optionsButton.interactable = true;
                creditsButton.interactable = true;
                exitButton.interactable = true;
            }
        }

    }

    public void OnStartPressed() 
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void OnOptionsPressed()
    {
        startButton.interactable = false; 
        optionsButton.interactable = false; 
        creditsButton.interactable = false;
        exitButton.interactable = false;

        optionsMenu.SetActive(true);
    }

    public void OnCreditsPressed()
    {
        startButton.interactable = false;
        optionsButton.interactable = false;
        creditsButton.interactable = false;
        exitButton.interactable = false;

        creditsMenu.SetActive(true);
    }

    public void OnExitPressed()
    {
#if UNITY_EDITOR
        // This will stop play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
