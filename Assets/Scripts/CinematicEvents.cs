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
    private bool waitingToSwitchScene = false;

    void Start()
    {
        director = GetComponent<PlayableDirector>();

        switch (cinematicName)
        {
            case CinematicName.Opening:
            {
                director.played += OnCinematicStopped;
            }
            break;
        }
    }

    private void Update()
    {
        switch (cinematicName)
        {
            case CinematicName.Opening:
                {
                    if (waitingToSwitchScene)
                    {
                        if (Input.anyKeyDown)
                        {
                            SceneManager.LoadScene(sceneBuildIndex: 1);
                        }
                    }
                }
                break;
        }
    }

    private void OnCinematicStopped(PlayableDirector director)
    {
        switch (cinematicName)
        {
            case CinematicName.Opening:
            {
                waitingToSwitchScene = true;
            }
            break;
        }
    }

}
