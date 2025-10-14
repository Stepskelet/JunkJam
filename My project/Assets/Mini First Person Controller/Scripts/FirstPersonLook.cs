using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    public float startX;

    public float startY;

    Vector2 velocity;
    Vector2 frameVelocity;

    bool mouseMoved = false;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {

        velocity = new Vector2(startX, startY);

        // Устанавливаем стартовый угол
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        if (!mouseMoved)
        {
            if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
            {
                mouseMoved = true;
            }
            else
            {
                transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
                character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
                return;
            }
        }


        // Get smooth velocity.
        if(!MainMenu.mainMenuActive&&!PauseMenu.isPaused)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
    }
}
