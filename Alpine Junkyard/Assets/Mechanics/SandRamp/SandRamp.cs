using Mirror;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class SandRamp : NetworkBehaviour
{
    [SerializeField] private float destroyAfter = 4;
    [SerializeField] private float spawnDuration = 2;

    private Vector3 startPos;
    private Vector3 endPos;
    private float spawnTimestamp;
    private Coroutine? currentCoroutine = null;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        endPos = transform.position;
        transform.position = startPos;
        //transform.rotation = transform.rotation;

        //rigidBody.AddForce(transform.forward * force);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SummonAnimation());
    }

    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.UnSpawn(gameObject);
        PooledTutorialObject.singleton.Return(gameObject);
    }

    // ServerCallback because we don't want a warning
    // if OnTriggerEnter is called on the client
    //[ServerCallback]
    //void OnTriggerEnter(Collider co) => DestroySelf();

    private IEnumerator SummonAnimation()
    {
        spawnTimestamp = Time.time;
        transform.position = startPos;

        var timeElapsed = 0f;
        while (timeElapsed < spawnDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / spawnDuration);
            timeElapsed = Time.time - spawnTimestamp;
            yield return null;
        }

        transform.position = endPos;
    }
}
