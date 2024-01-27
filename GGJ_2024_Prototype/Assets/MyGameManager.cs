using Complete;
using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerJoined(PlayerInput player)
    {
        var spawnPosition = startPositions[_currentPositionIndex].position;
        player.transform.position = spawnPosition;
        _currentPositionIndex++;
        if (_currentPositionIndex >= startPositions.Length)
        {
            _currentPositionIndex = 0;
        }

        SetCameraTargets(player.gameObject);
    }

    private void SetCameraTargets(GameObject gameObject)
    {
        _targets.Add(gameObject.transform);
        m_CameraControl.m_Targets = _targets.ToArray();
    }
}
