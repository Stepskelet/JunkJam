using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    [Tooltip("Maximum distance from the ground.")]
    public float distanceThreshold = .15f;

    [Tooltip("Whether this transform is grounded now.")]
    public bool isGrounded = true;
    /// <summary>
    /// Called when the ground is touched again.
    /// </summary>
    public event System.Action Grounded;

    public CapsuleCollider characterCollider;

    void LateUpdate()
    {
        if (characterCollider == null) return;

        // Получаем центр капсулы в мировых координатах
        Vector3 center = characterCollider.transform.TransformPoint(characterCollider.center);
        float height = Mathf.Max(characterCollider.height, characterCollider.radius * 2f);
        float halfHeight = (height / 2f) - characterCollider.radius;
        Vector3 up = characterCollider.transform.up;

        Vector3 point1 = center + up * halfHeight;
        Vector3 point2 = center - up * halfHeight;

        // Проверяем чуть ниже капсулы
        float castDistance = distanceThreshold;

        bool isGroundedNow = Physics.CapsuleCast(
            point1,
            point2,
            characterCollider.radius * 0.95f,
            Vector3.down,
            out RaycastHit hit,
            castDistance + 0.02f // небольшой запас
        );

        // Call event if we were in the air and we are now touching the ground.
        if (isGroundedNow && !isGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        isGrounded = isGroundedNow;
    }

    void OnDrawGizmosSelected()
    {
        if (characterCollider == null) return;

        Vector3 center = characterCollider.transform.TransformPoint(characterCollider.center);
        float height = Mathf.Max(characterCollider.height, characterCollider.radius * 2f);
        float halfHeight = (height / 2f) - characterCollider.radius;
        Vector3 up = characterCollider.transform.up;

        Vector3 point1 = center + up * halfHeight;
        Vector3 point2 = center - up * halfHeight;

        Gizmos.color = isGrounded ? Color.white : Color.red;
        Gizmos.DrawLine(point1, point1 + Vector3.down * (distanceThreshold + 0.02f));
        Gizmos.DrawLine(point2, point2 + Vector3.down * (distanceThreshold + 0.02f));
        Gizmos.DrawWireSphere(point1 + Vector3.down * (distanceThreshold + 0.02f), characterCollider.radius * 0.95f);
        Gizmos.DrawWireSphere(point2 + Vector3.down * (distanceThreshold + 0.02f), characterCollider.radius * 0.95f);
    }
}
