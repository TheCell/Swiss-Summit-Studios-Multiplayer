using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    private float _startTimeStamp;
    private bool _isRestarting;

    void Start()
    {
        _startTimeStamp = Time.time;
    }

    void Update()
    {
        if (_isRestarting)
        {
            return;
        }

        if (_startTimeStamp + 5 < Time.time)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        _isRestarting = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
