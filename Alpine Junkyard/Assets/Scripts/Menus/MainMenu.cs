using Mirror;
using System.Net;
using UnityEngine;

#nullable enable

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject? landingPagePanel = null;
    [SerializeField] private bool useSteam = false;

    public void HostServerAndClient()
    {
        if (!NetworkClient.active)
        {
            NetworkManager.singleton.StartHost();
        }
    }

    public void HostServer()
    {
        if (!NetworkClient.active)
        {
            NetworkManager.singleton.StartServer();
        }
    }

    public void JoinLobby(string networkAddress)
    {
        if (!NetworkClient.active)
        {
            NetworkManager.singleton.networkAddress = networkAddress;
            NetworkManager.singleton.StartClient();
        }
    }
}
