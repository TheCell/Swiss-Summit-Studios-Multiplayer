using Complete;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] private CameraControl m_CameraControl;
    [SerializeField] private Color32[] playerColors;
    private List<Transform> _targets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewPlayerSpawned()
    {

    }

    public void OnPlayerJoined(PlayerInput player)
    {
        SetCameraTargets(player.gameObject);
    }

    private void SetCameraTargets(GameObject gameObject)
    {
        _targets.Add(gameObject.transform);
        //// Create a collection of transforms the same size as the number of tanks.
        //Transform[] targets = new Transform[m_Tanks.Length];

        //// For each of these transforms...
        //for (int i = 0; i < targets.Length; i++)
        //{
        //    // ... set it to the appropriate tank transform.
        //    targets[i] = m_Tanks[i].m_Instance.transform;
        //}

        //// These are the targets the camera should follow.
        m_CameraControl.m_Targets = _targets.ToArray();
    }
}
