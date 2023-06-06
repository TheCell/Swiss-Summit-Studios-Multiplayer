using Mirror;
using Shapes;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    Disc? playerColorDisc = null;

    void OnEnable()
    {
        playerColorDisc = GetComponent<Disc>();
        if (playerColorDisc != null)
        {
            playerColorDisc.enabled = false;
        }
    }

    [Server]
    public void SetPlayerColor(Color color)
    {
        if (playerColorDisc == null)
        {
            Debug.LogError("No Disc object found to colorize");
            return;
        }

        playerColorDisc.enabled = true;
        playerColorDisc.Color = color;
    }
}
