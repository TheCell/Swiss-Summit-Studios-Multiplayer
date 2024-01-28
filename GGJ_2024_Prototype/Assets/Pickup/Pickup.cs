using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject _lantern;
    [SerializeField] ParticleSystem _lanternPickedParticleSystem;
    [SerializeField] ParticleSystem _lanternNotPickedYetParticleSystem;

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
        if (_lantern == null || !_lantern.activeSelf)
        {
            return;
        }

        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.HasLantern)
            {
                return;
            }

            playerController.PickupLantern();
            _lantern.SetActive(false);
            _lanternPickedParticleSystem.Play();
            _lanternNotPickedYetParticleSystem.Stop();
        }
    }
}
