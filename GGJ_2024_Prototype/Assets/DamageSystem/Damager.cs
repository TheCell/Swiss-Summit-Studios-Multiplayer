using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] bool _doesAoeDamage = false;
    [SerializeField] int _damageAmount = 1;
    [SerializeField] float _damageRadius = 5f;

    public void ApplyDamageToOthers()
    {
        if (_doesAoeDamage)
        {
            ApplyAoeDamage();
        }
    }

    private void ApplyAoeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _damageRadius);

        foreach (Collider collider in colliders)
        {
            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(_damageAmount);
            }
        }
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, _damageRadius);
    //}
}
