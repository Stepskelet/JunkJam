using UnityEngine;

public class PersonOrientation : MonoBehaviour
{
    public float angleY;

    public FirstPersonLook firstPersonLook;

    void Start()
    {
        firstPersonLook.enabled = false;
        transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }
}
