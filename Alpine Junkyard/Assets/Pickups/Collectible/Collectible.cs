using Assets.Scripts.Global;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

public class Collectible : NetworkBehaviour
{
    [SyncVar]
    public CollectibleType collectibleType;

    [System.Serializable]
    private class CollectibleEntry
    {
        public CollectibleType Type;
        public GameObject Prefab;
    }

    [SerializeField] private List<CollectibleEntry> collectibleEntries;

    public void Start()
    {
        var entriesOfType = collectibleEntries.Where((entry) => entry.Type == collectibleType).ToList();
        var randomNetworkDeterministic = new System.Random(unchecked((int)netId));
        Utility.Shuffle(entriesOfType, randomNetworkDeterministic);
        var entry = entriesOfType.FirstOrDefault();
        if (entry != null)
        {
            Instantiate(entry.Prefab, transform);
        }
        else
        {
            Debug.LogError($"No entries for {collectibleType} provided");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var collector = other.GetComponent<Collector>();
        if (collector != null )
        {
            if (collector.CanCollect(collectibleType))
            {
                collector.Add(collectibleType);
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }
}
