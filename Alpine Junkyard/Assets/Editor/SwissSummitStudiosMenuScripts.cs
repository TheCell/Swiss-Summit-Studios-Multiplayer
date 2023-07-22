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
        var mainMenu = GameObject.FindFirstObjectByType<MainMenu>();

        if (roomManager.transport == fizzySteamworks)
        {
            kcpTransport.enabled = true;
            roomManager.transport = kcpTransport;
            fizzySteamworks.enabled = false;
            mainMenu.useSteam = false;
            steamManager.enabled = false;
        }
        else
        {
            fizzySteamworks.enabled = true;
            steamManager.enabled = true;
            roomManager.transport = fizzySteamworks;
            mainMenu.useSteam = true;
            kcpTransport.enabled = false;
        }

        Debug.Log($"Switched to {roomManager.transport}");
        EditorUtility.SetDirty(roomManager);
        EditorUtility.SetDirty(kcpTransport);
        EditorUtility.SetDirty(fizzySteamworks);
        EditorUtility.SetDirty(steamManager);
        EditorUtility.SetDirty(mainMenu);
    }
}
