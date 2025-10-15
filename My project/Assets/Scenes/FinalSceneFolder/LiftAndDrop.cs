using System.ComponentModel;
using UnityEngine;

public class LiftAndDrop : MonoBehaviour
{
    public float liftHeight = 0.1f;      // На сколько поднять
    public float moveDuration = 1f;    // Время подъёма/опускания

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float elapsed = 0f;
    public bool isMoving = false;

    public bool isUp = false;

    public bool isDown = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void LiftUp()
    {
        if (!isMoving)
        {
            targetPosition = initialPosition + Vector3.up * liftHeight;
            elapsed = 0f;
            isMoving = true;
        }
    }

    public void PutDown()
    {
        if (!isMoving)
        {
            targetPosition = initialPosition;
            elapsed = 0f;
            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            transform.position = Vector3.Lerp(transform.position, targetPosition, t);

            if (t >= 1f)
            {
                transform.position = targetPosition;
                isMoving = false;

                if (!isDown && !isUp)
                {
                    isUp = true;
                }

                else if (isUp)
                {
                    isUp = false;
                    isDown = true;
                }
            }
        }
    }
}
