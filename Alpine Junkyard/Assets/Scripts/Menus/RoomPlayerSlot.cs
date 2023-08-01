using TMPro;
using UnityEngine;

#nullable enable

public class RoomPlayerSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text playerSlotText;
    private string emptySlotText = "Waiting For Player...";

    public void SetSlotPlayer(string? displayName = null)
    {
        playerSlotText.text = displayName ?? emptySlotText;
    }
}
