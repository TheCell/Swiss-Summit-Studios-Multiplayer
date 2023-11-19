using System;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private Dictionary<CollectibleType, int> collection = new();
    public event Action<CollectibleType> itemCollected;
    public event Action<CollectibleType> itemRemoved;
    //private Transform animationSpawn; // todo visualise pickup or remove with shapes

    public void Add(CollectibleType type)
    {
        collection.TryGetValue(type, out var amount);
        collection[type] = amount + 1;
        itemCollected?.Invoke(type);
    }

    public void Remove(CollectibleType type)
    {
        collection.TryGetValue(type, out var amount);
        if (amount > 0)
        {
            collection[type] = amount - 1;
            itemRemoved?.Invoke(type);
        }
    }

    public bool CanCollect(CollectibleType type)
    {
        return !IsLimitReached(type);
    }

    private bool IsLimitReached(CollectibleType type)
    {
        switch (type)
        {
            case CollectibleType.Coin:
                return false;
            case CollectibleType.Trash:
                collection.TryGetValue(CollectibleType.Trash, out var trash);
                return trash > 2;
            case CollectibleType.Snack:
                collection.TryGetValue(CollectibleType.Snack, out var snacks);
                return snacks > 1;
            default:
                throw new NotImplementedException();
        }
    }
}
