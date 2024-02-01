using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public enum CinematicName
{
    Opening = 0,
    Ending
}

public class CinematicEvents : MonoBehaviour
{
    [SerializeField] private CinematicName cinematicName = CinematicName.Opening;
    private PlayableDirector director;
    private bool cinematicStarted = false;

    void Start()
    {
        director = GetComponent<PlayableDirector>();

        switch (cinematicName)
        {
            case CinematicName.Opening:
            {
                director.Pause();
            }
            break;
        }
    }

    void Update()
    {
        switch (cinematicName)
        {
            case CinematicName.Opening:
            {
                if (cinematicStarted)
                {
                    if (director.state != PlayState.Playing)
                    {
                         SceneManager.LoadScene(sceneBuildIndex: 1);
                    }
                }
                else
                {
                    if (Input.anyKeyDown)
                    {
                        director.Play();
                        cinematicStarted = true;
                    }
                }
            }
            break;
        }
    }
}
