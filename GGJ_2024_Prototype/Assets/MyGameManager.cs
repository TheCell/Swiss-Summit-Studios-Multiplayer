using Complete;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] private CameraControl m_CameraControl;
    [SerializeField] private Color32[] playerColors;
    [SerializeField] private Transform[] startPositions;
    private int _currentPositionIndex = 0;
    private List<Transform> _targets = new List<Transform>();

    public void OnPlayerJoined(PlayerInput player)
    {
        var spawnPosition = GetNextSpawn().position;
        player.transform.position = spawnPosition;

        SetCameraTargets(player.gameObject);
        player.GetComponent<PlayerController>().onPlayerRespawned.AddListener(OnPlayerRespawned);
    }

    public void OnPlayerRespawned(GameObject player)
    {
        var spawnPosition = GetNextSpawn().position;
        player.transform.position = spawnPosition;
    }

    private void SetCameraTargets(GameObject gameObject)
    {
        _targets.Add(gameObject.transform);
        m_CameraControl.m_Targets = _targets.ToArray();
    }

    private Transform GetNextSpawn()
    {
        var spawnPosition = startPositions[_currentPositionIndex];
        _currentPositionIndex++;
        if (_currentPositionIndex >= startPositions.Length)
        {
            _currentPositionIndex = 0;
        }

        return spawnPosition;
    }
}
