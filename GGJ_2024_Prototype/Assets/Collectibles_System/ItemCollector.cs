using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private GameObject _holdingItem;
    [SerializeField] private int _capacity = 1;
    private int _currentCapacity = 0;

    public int CurrentlyHolding => _currentCapacity;

    public int DumpItems()
    {
        var count = _currentCapacity;
        _currentCapacity = 0;
        _holdingItem.SetActive(false);
        return count;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (_currentCapacity < _capacity)
            {
                _currentCapacity++;
                Destroy(other.gameObject);
                _holdingItem.SetActive(true);
            }
        }
    }
}
