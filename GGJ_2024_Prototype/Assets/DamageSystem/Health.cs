using UnityEngine;

public class Health : MonoBehaviour
{
    public VoidEvent onTookLethalDamage;
    public IntEvent onTookDamage;

    [SerializeField] private int _currentHealth = 10;

    public void SetHealth(int health)
    {
        _currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        onTookDamage?.Invoke(damage);

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            onTookLethalDamage?.Invoke();
        }
    }
}
