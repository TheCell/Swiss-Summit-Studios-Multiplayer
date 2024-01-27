using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 12f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isGrounded;

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
    }
}
