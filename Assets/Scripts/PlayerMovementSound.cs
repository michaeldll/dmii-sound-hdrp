using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    public CharacterController controller;
    public GameObject head;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float speed = 10f;
    public float rotationSpeed = 5f;
    public float gravity = -9.81f;
    public float headBobAmplitude = 0.5f;
    public float headBobSpeed = 0.1f;
    public bool enableControls = false;
    public bool enableMovementBySoundLevel = true;
    public DataObject data = null;
    public Vector3 velocity;
    public Vector3 damping;
    public Vector3 maxVelocity = new Vector3(10, 10, 10);
    public bool gameStartOnAwake = true;

    private Vector3 _acceleration;
    private float groundDistance = 0.4f;
    private bool isGrounded;
    private float time = 0f;

    void Awake(){
        data.SetVolume(0f);
        data.SetGameStarted(gameStartOnAwake);
    }

    
    public Vector3 ClampVector(Vector3 v, Vector3 min, Vector3 max)
    {
        return new Vector3(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (data.isGameStarted) 
        {
            if (enableControls)
            {
                _acceleration.x = Input.GetAxis("Horizontal");
                _acceleration.z = Input.GetAxis("Vertical");
            }

            if (data && enableMovementBySoundLevel)
            {
                _acceleration.z = data.micVolumeNormalized;
            }
        }

        // Head Bob
        head.transform.localPosition = new Vector3(0, Mathf.Sin(time * headBobSpeed) * headBobAmplitude * _acceleration.z, 0);

        // Movements
        Vector3 move = transform.forward * _acceleration.z;
        Vector3 moveDirection = new Vector3(0, _acceleration.x, 0);

        controller.Move(move * speed * Time.deltaTime);
        transform.Rotate(moveDirection * rotationSpeed * Time.deltaTime);

        //Not used but might be useful
        //Vector3 direction = new Vector3(_acceleration.x, 0, _acceleration.z).normalized;
        //float targetAngle;
        //if (direction.magnitude > 0.1f)
        //{
        //targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //}

        // Gravity
        // DeltaTime is squared because of physics gravity function.
        if(!isGrounded) _acceleration.y += gravity * Time.deltaTime;
        velocity = _acceleration;
        ClampVector(velocity, new Vector3(0, 0, 0), maxVelocity);
        controller.Move(velocity * Time.deltaTime);

        time += 1f * Time.deltaTime;
    }
}
