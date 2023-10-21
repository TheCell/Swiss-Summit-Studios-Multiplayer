using Mirror;
using System.Collections;
using UnityEngine;

#nullable enable

public class SummonRamp : MonoBehaviour, ISpell
{
    [SerializeField] private GameObject rampPrefab;
    private GameObject instance;

    [SerializeField] private float spawnDuration = 2;
    private float spawnTimestamp;
    private Vector3 startPos;
    private Vector3 endPos;

    private Coroutine? currentCoroutine = null;

    //[Command]
    public GameObject GetObjectToSpawnOnServer()
    {
        return instance;
    }

    public void Start()
    {
        instance = Instantiate<GameObject>(rampPrefab, new Vector3(0, -1000, 0), Quaternion.identity);
    }

    public void CastSpell(Transform startPosition)
    {
        startPos = new Vector3(startPosition.position.x, startPosition.position.y - 3, startPosition.position.z);
        endPos = startPosition.position;
        instance.transform.position = startPos;
        instance.transform.rotation = startPosition.rotation;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SummonAnimation());
    }

    private IEnumerator SummonAnimation()
    {
        spawnTimestamp = Time.time;
        instance.transform.position = startPos;

        var timeElapsed = 0f;
        while (timeElapsed < spawnDuration)
        {
            instance.transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / spawnDuration);
            timeElapsed = Time.time - spawnTimestamp;
            yield return null;
        }

        instance.transform.position = endPos;
    }
}
