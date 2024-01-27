using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _maxPos;
    [SerializeField] private Transform _minPos;
    [SerializeField] private GameObject _platform;
    //private Transform currentPosition;

    private List<GameObject> objectsInCollider = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //currentPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var currentPos = _platform.transform.localPosition;
        if (objectsInCollider.Count > 0)
        {
            _platform.transform.localPosition = Vector3.MoveTowards(currentPos, _maxPos.localPosition, _moveSpeed * Time.deltaTime);
        }
        else
        {
            _platform.transform.localPosition = Vector3.MoveTowards(currentPos, _minPos.localPosition, _moveSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        objectsInCollider.Add(other.gameObject);
        //Debug.Log($"There are currently {objectsInCollider.Count} objects in the collider");
    }

    public void OnTriggerExit(Collider other)
    {
        objectsInCollider.Remove(other.gameObject);
        //Debug.Log($"There are currently {objectsInCollider.Count} objects in the collider");
    }
}
