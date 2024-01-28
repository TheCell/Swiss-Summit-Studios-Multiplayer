using UnityEngine;

public class Activatable : MonoBehaviour
{
    public VoidEvent OnActivation;
    [SerializeField] int _needsLanternCount = 1;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject _buttonToBePressed;

    private int _currentLanternCount = 0;

    public void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.HasLantern)
            {
                _currentLanternCount++;
                CheckAndActivateLantern();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.HasLantern)
            {
                _currentLanternCount--;
            }
        }
    }

    private void CheckAndActivateLantern()
    {
        if (_buttonToBePressed.activeSelf)
        {
            return;
        }

        if (_currentLanternCount >= _needsLanternCount)
        {
            _particleSystem.Play();
            _buttonToBePressed.SetActive(false);
            OnActivation.Invoke();
        }
    }
}
