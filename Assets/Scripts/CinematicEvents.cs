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

    void Start()
    {

        switch (cinematicName)
        {
            case CinematicName.Opening:
            {
                AudioManager.Instace.MuteBGM();
            }
            break;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
            AudioManager.Instace.UnMuteBGM();
        }
    }
}
