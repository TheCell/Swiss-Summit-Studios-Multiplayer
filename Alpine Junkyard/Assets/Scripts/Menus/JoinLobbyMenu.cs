using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject? landingPagePanel = null;
    [SerializeField] private TMP_InputField? addressInput = null;
    [SerializeField] private Button? joinButton = null;
    [SerializeField] private MainMenu menu;

    public void Join()
    {
        var address = addressInput.text;
        menu.JoinLobby(address);
    }
}
