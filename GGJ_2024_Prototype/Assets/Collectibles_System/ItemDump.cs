using UnityEngine;

public class ItemDump : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToBeActivated;
    [SerializeField] private GameObject _lockSymbol;
    private int _currentActivatedItemIndex = 0;

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
        var isItemHolder = other.GetComponent<ItemHolder>();
        if (isItemHolder != null)
        {
            if (isItemHolder.CurrentlyHolding + _currentActivatedItemIndex -1 >= itemsToBeActivated.Length)
            {
                return;
            }

            var count = isItemHolder.DumpItems();
            for (int i = 0; i < count; i++)
            {
                ActivateItem();
            }

            if (_currentActivatedItemIndex >= itemsToBeActivated.Length)
            {
                _lockSymbol.SetActive(false);
            }
        }
    }

    private void ActivateItem()
    {
        if (_currentActivatedItemIndex < itemsToBeActivated.Length)
        {
            itemsToBeActivated[_currentActivatedItemIndex].SetActive(true);
            _currentActivatedItemIndex++;
        }
    }
}
