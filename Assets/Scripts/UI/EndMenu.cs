using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayAgainPressed()
    {
        SceneManager.LoadScene("LevelScene");
    }
    
    public void onRetunMainMenuPressed()
    {
        SceneManager.LoadScene("StartScene");
    }


}
