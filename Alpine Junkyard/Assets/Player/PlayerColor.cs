using Shapes;
using UnityEngine;

#nullable enable

public class PlayerColor : MonoBehaviour
{
    Disc playerColorDisc;
    NetworkGamePlayer networkGamePlayer;

    void Start()
    {
        playerColorDisc = GetComponent<Disc>();
        networkGamePlayer = GetComponentInParent<NetworkGamePlayer>();

        if (playerColorDisc != null && networkGamePlayer != null )
        {
            playerColorDisc.Color = networkGamePlayer.PlayerColor;
        }
        else
        {
            Debug.LogError("playerColorDisc or networkGamePlayer not found");
        }
    }
}
