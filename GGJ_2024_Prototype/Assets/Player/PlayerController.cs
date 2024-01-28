using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public delegate void OnPlayerRespawned(GameObject player);
    public OnPlayerRespawned onPlayerRespawned;

    [SerializeField] float _speed = 12f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _objectToDisable;

    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private bool _hasHitRespawnTrigger;
    private bool _isDead;

    // Start is called before the first frame update
    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_animator == null)
        {
            Debug.LogError("Animator not set");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (_isDead)
        {
            return;
        }

        if (ShouldRespawn())
        {
            StartRespawn();
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        _movementInputValue = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    bool _jumpRequested;
    Vector2 _movementInputValue;
    private void Move()
    {
        var movement = new Vector3(_movementInputValue.x, 0f, _movementInputValue.y) * _speed * Time.deltaTime;
        _animator.SetFloat("Speed", movement.magnitude);
        _rigidbody.MovePosition(_rigidbody.position + movement);

        var jump = new Vector3(0f, _jumpRequested ? _jumpForce : 0f, 0f);
        _animator.SetBool("isGrounded", _isGrounded);
        if (_jumpRequested)
        {
            _isGrounded = false;
        }
        _rigidbody.AddForce(jump, ForceMode.Impulse);
        _jumpRequested = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Killtrigger"))
        {
            _hasHitRespawnTrigger = true;
        }
    }

    private bool ShouldRespawn()
    {
        if (transform.position.y < -10f)
        {
            return true;
        }

        if (_hasHitRespawnTrigger)
        {
            return true;
        }

        return false;
    }

    private void StartRespawn()
    {
        _isDead = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        StartCoroutine(Respawn());
        _objectToDisable.SetActive(false);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        transform.position = Vector3.zero;
        _objectToDisable.SetActive(true);
        _hasHitRespawnTrigger = false;
        _isDead = false;
        _rigidbody.isKinematic = false;
        onPlayerRespawned?.Invoke(gameObject);
    }
}
