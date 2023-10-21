using UnityEngine;

public interface ISpell
{
    //public abstract void ServerInitialize();
    //public abstract GameObject GetObjectToSpawnOnServer();

    public abstract void CastSpell(Transform transform);
}
