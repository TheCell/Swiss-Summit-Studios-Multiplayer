using Mirror;
using Steamworks;
using System.Net;
using UnityEngine;

#nullable enable

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject? landingPagePanel = null;
    [SerializeField] private bool useSteam = false;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private void Start()
    {
        if (!useSteam)
        {
            return;
        }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            // Steam failed
            Debug.LogError("Steam failed to create a Lobby");
            return;
        }

        NetworkManager.singleton.StartHost();
        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            "HostAddress",
            SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        Debug.Log("NetworkServer.active: " + NetworkServer.active);
        if (NetworkServer.active)
        {
            // As the server we don't want to do anything here
            return;
        }

        Debug.Log(callback.m_ulSteamIDLobby);
        var hostAddress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            "HostAddress");
        JoinLobby(hostAddress);
    }

    public void HostServerAndClient()
    {
        if (useSteam)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 6);
            return;
        }

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
        Debug.Log("networkAddress: " + networkAddress);
        if (!NetworkClient.active)
        {
            NetworkManager.singleton.networkAddress = networkAddress;
            NetworkManager.singleton.StartClient();
        }
    }
}
