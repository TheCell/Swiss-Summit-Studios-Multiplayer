using UnityEngine;
using System.Collections;
using Mirror;

#nullable enable

public class PlayerEquip : NetworkBehaviour
{
    [SerializeField] private GameObject sceneObjectPrefab;

    [SerializeField] private GameObject rightHand;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject cylinderPrefab;

    [SyncVar(hook = nameof(OnChangeEquipment))]
    public EquippedItem equippedItem;


    public void Update()
    {
        if (!isLocalPlayer) return;

        // TODO make it proper
        if (Input.GetKeyDown(KeyCode.Alpha0) && equippedItem != EquippedItem.nothing)
        {
            CmdChangeEquippedItem(EquippedItem.nothing);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && equippedItem != EquippedItem.ball)
        {
            CmdChangeEquippedItem(EquippedItem.ball);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && equippedItem != EquippedItem.box)
        {
            CmdChangeEquippedItem(EquippedItem.box);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && equippedItem != EquippedItem.cylinder)
        {
            CmdChangeEquippedItem(EquippedItem.cylinder);
        }

        if (Input.GetKeyDown(KeyCode.X) && equippedItem != EquippedItem.nothing)
        {
            CmdDropItem();
        }
    }

    // CmdPickupItem is public because it's called from a script on the SceneObject
    [Command]
    public void CmdPickupItem(GameObject sceneObject)
    {
        if (equippedItem != EquippedItem.nothing)
        {
            CmdDropItem();
        }

        // set the player's SyncVar so clients can show the equipped item
        equippedItem = sceneObject.GetComponent<SceneObject>().equippedItem;

        // Destroy the scene object
        NetworkServer.Destroy(sceneObject);
    }

    private void OnChangeEquipment(EquippedItem oldEquippedItem, EquippedItem newEquippedItem)
    {
        StartCoroutine(ChangeEquipment(newEquippedItem));
    }

    // Since Destroy is delayed to the end of the current frame, we use a coroutine
    // to clear out any child objects before instantiating the new one
    private IEnumerator ChangeEquipment(EquippedItem newEquippedItem)
    {
        while (rightHand.transform.childCount > 0)
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            yield return null;
        }

        switch (newEquippedItem)
        {
            case EquippedItem.ball:
                var ball = Instantiate(ballPrefab, rightHand.transform);
                ball.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case EquippedItem.box:
                var box = Instantiate(boxPrefab, rightHand.transform);
                box.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case EquippedItem.cylinder:
                var cylinder = Instantiate(cylinderPrefab, rightHand.transform);
                cylinder.GetComponent<Rigidbody>().isKinematic = true;
                break;
        }
    }

    [Command]
    private void CmdChangeEquippedItem(EquippedItem selectedItem)
    {
        equippedItem = selectedItem;
    }

    [Command]
    private void CmdDropItem()
    {
        // Instantiate the scene object on the server
        Vector3 pos = rightHand.transform.position;
        Quaternion rot = rightHand.transform.rotation;
        GameObject newSceneObject = Instantiate(sceneObjectPrefab, pos, rot);

        // set the RigidBody as non-kinematic on the server only (isKinematic = true in prefab)
        //var rigidbody = newSceneObject.GetComponentInChildren<Rigidbody>();
        //if (rigidbody)
        //{
        //    Debug.Log("GetComponentInChildren");
        //    rigidbody.isKinematic = false;
        //}
        //var rigidbodyTest = newSceneObject.GetComponent<Rigidbody>();
        //if (rigidbodyTest)
        //{
        //    Debug.Log("GetComponent");
        //    rigidbodyTest.isKinematic = false;
        //}

        SceneObject sceneObject = newSceneObject.GetComponent<SceneObject>();

        // set the child object on the server
        sceneObject.SetEquippedItem(equippedItem);

        // set the SyncVar on the scene object for clients
        sceneObject.equippedItem = equippedItem;

        // set the player's SyncVar to nothing so clients will destroy the equipped child item
        equippedItem = EquippedItem.nothing;

        // Spawn the scene object on the network for all to see
        NetworkServer.Spawn(newSceneObject);
    }
}