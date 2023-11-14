using Mirror;
using UnityEngine;

public class PickupSpawner : NetworkBehaviour
{
    void Start()
    {
        var sceneObjects = FindObjectsByType<SceneObject>(FindObjectsSortMode.None);
        foreach (var sceneObject in sceneObjects)
        {
            sceneObject.SetEquippedItem(sceneObject.equippedItem);
            //NetworkServer.Spawn(sceneObject.gameObject);
        }

        Destroy(this);
    }
}
