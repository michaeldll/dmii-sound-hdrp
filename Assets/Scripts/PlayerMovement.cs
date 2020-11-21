using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public LayerMask _groundLayer;

    [SerializeField]
    public float _speed = 10f;

    [SerializeField]
    public float _rotationSpeed = 5f;

    [SerializeField]
    public float _gravity = -9.81f;

    [SerializeField]
    public float _headBobAmplitude = 0.5f;

    [SerializeField]
    public float _headBobSpeed = 0.1f;

    private Transform _head;
    private CharacterController _controller;
    private Transform _groundCheck;
    private Vector3 _velocity;
    private float _groundDistance = 0.4f;
    private bool _isGrounded;
    private float _time = 0f;
    private Vector3 _startHeadPosition;

    // Public
    public void InitPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

    // Hooks
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _head = transform.Find("Head");
        _groundCheck = transform.Find("Ground Check");
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Head Bob
        float offsetY = Mathf.Sin(_time * _headBobSpeed) * _headBobAmplitude * z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        // Movements
        Vector3 move = transform.forward * z;
        Vector3 moveDirection = new Vector3(0, x, 0);

        _controller.Move(move * _speed * Time.deltaTime);
        transform.Rotate(moveDirection * _rotationSpeed * Time.deltaTime);

        // Not used but might be useful in the future
        Vector3 direction = new Vector3(x, 0, z).normalized;
        float targetAngle;
        if (direction.magnitude > 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }

        // Gravity
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        _time += 1f * Time.deltaTime;
    }

    void Start()
    {
        _startHeadPosition = new Vector3(_head.position.x, _head.position.y, _head.position.z);
    }
}
