using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkGamePlayer : NetworkBehaviour
{
    public Color overlayColor = new Color(0, 0, 0, 0.5f);

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
    }

    [SyncVar][SerializeField] private string displayName = "Missing Name";

    public string DisplayName { get => displayName; }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    // animation variables for playerwalking
    [SyncVar] public float _animationBlend;
    [SyncVar] public float _inputMagnitude;

    [Command]
    public void CmdUpdateAnimations(float animationBlend, float inputMagnitude)
    {
        _animationBlend = animationBlend;
        _inputMagnitude = inputMagnitude;
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public void OnGUI()
    {
        if (!Camera.main) return;

        // show data next to player for easier debugging. this is very useful!
        // IMPORTANT: this is basically an ESP hack for shooter games.
        //            DO NOT make this available with a hotkey in release builds
        if (!Debug.isDebugBuild) return;

        // project position to screen
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);

        // enough alpha, in front of camera and in screen?
        if (point.z >= 0 && Utils.IsPointInScreen(point))
        {
            GUI.color = overlayColor;
            GUILayout.BeginArea(new Rect(point.x, Screen.height - point.y, 200, 100));

            // always show both client & server buffers so it's super
            // obvious if we accidentally populate both.
            GUILayout.Label("");
            GUILayout.Label("");
            GUILayout.Label($"{displayName}");

            GUILayout.EndArea();
            GUI.color = Color.white;
        }
    }
#endif

}
