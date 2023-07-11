using kcp2k;
using Mirror.FizzySteam;
using UnityEditor;
using UnityEngine;

public class SwissSummitStudiosMenuScripts
{
    [MenuItem("SSS/Toggle Networking %&s")]
    private static void ToggleNetworking()
    {
        var roomManager = GameObject.FindFirstObjectByType<AlpineJunkyardNetworkRoomManager>();
        var kcpTransport = GameObject.FindFirstObjectByType<KcpTransport>();
        var fizzySteamworks = GameObject.FindFirstObjectByType<FizzySteamworks>();
        var steamManager = GameObject.FindFirstObjectByType<SteamManager>();

        if (roomManager.transport == fizzySteamworks)
        {
            kcpTransport.enabled = true;
            roomManager.transport = kcpTransport;
            fizzySteamworks.enabled = false;
            steamManager.enabled = false;
        }
        else
        {
            fizzySteamworks.enabled = true;
            steamManager.enabled = true;
            roomManager.transport = fizzySteamworks;
            kcpTransport.enabled = false;
        }

        Debug.Log($"Switched to {roomManager.transport}");
    }
}
