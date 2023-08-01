using Mirror;
using TMPro;
using UnityEngine;

#nullable enable

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject? lobbyUI = null;
    [SerializeField] private TMP_Text? statusText = null;
    private bool isReady = false;

    [SerializeField]
    private RoomPlayerSlot[] roomPlayerSlots = new RoomPlayerSlot[6];

    private void Start()
    {
        UpdateStatusLabel();
    }

    public void SetSlotPlayer(string playerName, int slotNumber)
    {
        roomPlayerSlots[slotNumber].SetSlotPlayer(playerName);
    }

    public void ResetSlots()
    {
        for (int i = 0; i < roomPlayerSlots.Length; i++)
        {
            roomPlayerSlots[i].SetSlotPlayer();
        }
    }

    public void StopClient()
    {
        NetworkManager.singleton.StopClient();
    }

    public void StartGame()
    {
    }

    public void SetReadyState()
    {
        // TODO
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

    public void KickPlayer()
    {
        // TODO
        //if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
        //{
        //    // This button only shows on the Host for all players other than the Host
        //    // Host and Players can't remove themselves (stop the client instead)
        //    // Host can kick a Player this way.
        //    GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        //}
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
