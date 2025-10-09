using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    public float stepHeight = 0.5f;
    public float stepSmooth = 0.2f;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);

        
        RaycastHit hitLower;
        // Луч вперед на уровне ног
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, transform.forward, out hitLower, 0.5f))
        {
            RaycastHit hitUpper;

            float verticalInput = Input.GetAxis("Vertical");

            // Луч вперед на уровне чуть выше ступеньки
            if (!Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.forward, out hitUpper, 0.5f) 
            && verticalInput > 0)
            {
                // Поднимаем персонажа вверх
                rigidbody.position += new Vector3(0, stepSmooth, 0);
            }
        }
    }
}