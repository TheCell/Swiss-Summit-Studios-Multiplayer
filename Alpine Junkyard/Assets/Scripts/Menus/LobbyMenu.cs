using Mirror;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

public class LobbyMenu : MonoBehaviour
{
    //public Button? readyButton = null;
    //public Button? startButton = null;

    [SerializeField] private GameObject? lobbyUI = null;
    [SerializeField] private TMP_Text? statusText = null;
    //[SerializeField] private GameObject playerList = null;
    //[SerializeField] private GameObject roomPlayerSlot = null;
    private bool isReady = false;

    private List<AlpineJunkyardNetworkRoomPlayer> connectedPlayers = new List<AlpineJunkyardNetworkRoomPlayer>();

    private void Start()
    {
        //AlpineJunkyardNetworkRoomManager.OnRoomClientConnect
        //AlpineJunkyardNetworkRoomManager.ClientOnConnected += HandleClientConnected;
        UpdateStatusLabel();
    }

    //private void Update()
    //{

    //}

    //private void OnDestroy()
    //{
    //    AlpineJunkyardNetworkRoomManager.ClientOnConnected -= HandleClientConnected;
    //}

    public void StopClient()
    {
        NetworkManager.singleton.StopClient();
    }

    public void StartGame()
    {
    }

    public void SetReadyState()
    {
        isReady = !isReady;
        NetworkClient.connection.identity.GetComponent<AlpineJunkyardNetworkRoomPlayer>().CmdChangeReadyState(isReady);
    }

    public void LeaveLobby()
    {
        // Host: stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        // Client
        else if (!NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        // Server: stop server if server-only
        else if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }
    }

    private void UpdateStatusLabel()
    {
        // host mode
        // display separately because this always confused people:
        //   Server: ...
        //   Client: ...
        if (NetworkServer.active && NetworkClient.active)
        {
            statusText.text = $"<b>Host</b>: running via {Transport.active}";
        }
        // server only
        else if (NetworkServer.active)
        {
            statusText.text = $"<b>Server</b>: running via {Transport.active}";
        }
        // client only
        else if (NetworkClient.isConnected)
        {
            statusText.text = $"<b>Client</b>: connected to {NetworkManager.singleton.networkAddress} via {Transport.active}";
        }
    }
}
