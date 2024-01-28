using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WinLoseConditionTracker : MonoBehaviour
{
    private List<PlayerController> _players = new List<PlayerController>();
    private bool _isLoadingDeathScene;

    public void OnPlayerJoined(PlayerInput player)
    {
        _players.Add(player.GetComponent<PlayerController>());
    }

    public void OnGameWon()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Update()
    {
        if (_isLoadingDeathScene)
        {
            return;
        }

        if (_players.Count > 0)
        {
            var alivePlayers = _players.Where(p => !p.IsDead).ToList();
            if (alivePlayers.Count == 0)
            {
                _isLoadingDeathScene = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
    }
}
