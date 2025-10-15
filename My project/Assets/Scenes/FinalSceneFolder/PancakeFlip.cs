using UnityEngine;

public class PancakeFlip : MonoBehaviour
{
    public float jumpHeight = 2f;      // Насколько высоко подлетает
    public float flipDuration = 1f;    // Время анимации (секунды)
    public float flipAngle = 360f;     // На сколько градусов переворачивается (обычно 360)

    private Vector3 startPosition;
    private Quaternion startRotation;
    public bool flipping = false;
    public bool flipped = false;
    private float elapsed = 0f;

    void Start()
    {
        
    }

    public void Flip()
    {
        if (!flipping)
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            elapsed = 0f;
            flipping = true;
        }
    }

    void Update()
    {
        if (flipping)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / flipDuration);

            // Движение вверх и вниз (по параболе)
            float yOffset = 4 * jumpHeight * t * (1 - t); // парабола: максимум в середине

            // Поворот
            float angle = Mathf.Lerp(0, flipAngle, t);

            // Применяем позицию и поворот
            transform.position = startPosition + Vector3.up * yOffset;
            transform.rotation = startRotation * Quaternion.Euler(angle, 0, 0);

            if (t >= 1f)
            {
                // Возврат к исходному состоянию
                transform.position = startPosition;
                transform.rotation = startRotation * Quaternion.Euler(flipAngle, 0, 0);
                flipping = false;
                flipped = true;
            }
        }
    }
}
