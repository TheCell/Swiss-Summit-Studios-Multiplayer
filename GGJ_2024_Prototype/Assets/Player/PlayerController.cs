using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 12f;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void Walk(InputAction.CallbackContext context)
    {
        _movementInputValue = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    Vector2 _movementInputValue;
    private void Move()
    {
        var movement = new Vector3(_movementInputValue.x, 0f, _movementInputValue.y) * _speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }
}
