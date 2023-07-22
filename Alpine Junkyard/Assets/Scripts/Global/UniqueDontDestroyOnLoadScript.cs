using System;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static List<string> uniqueInstancedGameObjects = new List<string>();
    public static List<Guid> uniqueIdentifiers = new List<Guid>();

    private Guid uniqueIdentifier = Guid.NewGuid();

    void Awake()
    {
        if (transform.parent != null)
        {
            Debug.LogWarning("This Object will be destroyed anyway because it is a child of an other Object");
        }

        if (IsMyTypeRegistered(this.gameObject.name))
        {
            if (!IsTheOriginalInstance())
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            uniqueInstancedGameObjects.Add(this.gameObject.name);
            uniqueIdentifiers.Add(uniqueIdentifier);
        }
    }

    private void OnDestroy()
    {
        if (IsTheOriginalInstance())
        {
            uniqueIdentifiers.Remove(this.uniqueIdentifier);
            uniqueInstancedGameObjects.Remove(this.gameObject.name);
        }
    }

    private bool IsMyTypeRegistered(string name)
    {
        return uniqueInstancedGameObjects.Contains(name);
    }

    private bool IsTheOriginalInstance()
    {
        return uniqueIdentifiers.Contains(this.uniqueIdentifier);
    }
}
