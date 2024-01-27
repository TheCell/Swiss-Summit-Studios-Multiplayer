using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform maxPos;
    [SerializeField] private Transform minPos;

    private List<GameObject> objectsInCollider = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        
        objectsInCollider.Add(other.gameObject);
        Debug.Log($"There are currently {objectsInCollider.Count} objects in the collider");
    }

    public void OnTriggerExit(Collider other)
    {
        objectsInCollider.Remove(other.gameObject);
        Debug.Log($"There are currently {objectsInCollider.Count} objects in the collider");
    }
}
