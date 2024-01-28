using UnityEngine;
using UnityEngine.AI;

public class ChasingAgent : MonoBehaviour
{
    public VoidEvent onExplosion;

    [SerializeField] NavMeshAgent _agent = null;
    [SerializeField] GameObject _ghostSkinToHide = null;
    [SerializeField] float _scanChaseDistance = 10f;
    [SerializeField] float _maxChaseDistance = 40f;
    [SerializeField] float _scanInterval = 1f;
    [SerializeField] ParticleSystem _explosion = null;
    private float _killSelfTimestamp;
    private float _timestamp;
    private Transform _target;
    private bool exploding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (exploding)
        {
            if (_killSelfTimestamp < Time.time)
            {
                Destroy(gameObject);
            }
            return;
        }

        CheckTargetOutOfRange();
        ChasePlayer();

        if (_timestamp + _scanInterval < Time.time)
        {
            _timestamp = Time.time;
            ScanForPlayer();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>() ?? other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            exploding = true;
            if (_explosion != null)
            {
                _explosion.Play();
            }

            if (_ghostSkinToHide != null)
            {
                _ghostSkinToHide.SetActive(false);
            }

            onExplosion?.Invoke();
            _killSelfTimestamp = Time.time + _explosion.main.duration;
        }
    }

    private void ScanForPlayer()
    {
        var players = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];
            var distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < _scanChaseDistance)
            {
                _target = player.transform;
                _agent.SetDestination(player.transform.position);
                break;
            }
        }
    }

    private void ChasePlayer()
    {
        if (_target == null)
        {
            return;
        }

        _agent.SetDestination(_target.position);
    }

    private void CheckTargetOutOfRange()
    {
        if (_target == null)
        {
            return;
        }

        var distance = Vector3.Distance(_target.position, transform.position);
        if (distance > _maxChaseDistance)
        {
            _target = null;
        }
    }
}
