using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveToTarget : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;            // Камера игрока
    public Transform movePosition;         // Куда перемещаться

    public GameObject person;

    public Transform cameraPosition; // Камера персонажа
    public FirstPersonMovement movementScript; // Скрипт движение
    [SerializeField] private HintManager hint;
    public FirstPersonLook lookScript; // Скрипт камеры

    [Header("Interaction")]
    public float interactDistance = 2f;    // Дистанция взаимодействия

    private StoveIteraction stoveIteraction;

    private Vector3 savedPos;
    private Quaternion savedRot;
    private Quaternion cameraRot;
    private Rigidbody rb;
    private Collider col;
    private bool isMoving = false;
    private bool awaitingEscape = false;

    void Awake()
    {
        rb = person.GetComponent<Rigidbody>();
        col = person.GetComponent<Collider>();
    }

    void Start()
    {
        stoveIteraction = GetComponent<StoveIteraction>();
        stoveIteraction.enabled = false;
    } 

    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        if (Input.GetKeyDown(KeyCode.E) && !isMoving && !awaitingEscape)
        {
            Ray ray = playerCamera.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider.CompareTag(this.tag) || hit.collider.transform.root.CompareTag(this.tag))
                {
                    BeginMove();
                    stoveIteraction.enabled = true;
                }   
            }
        }
        Ray ray1 = playerCamera.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray1, out RaycastHit hit1, interactDistance))
        {
            if (hit1.collider.CompareTag(this.tag))
            {
                hint.ShowHint("E");
            }
            else
            {
                hint.HideHint();
            }
        }
        else
        {
            hint.HideHint();
        }

        if (awaitingEscape && Input.GetKeyDown(KeyCode.Escape))
        {
            stoveIteraction.Reset();
            awaitingEscape = false;
            RestoreState();
        }
    }

    private void BeginMove()
    {
        savedPos = person.transform.position;
        savedRot = person.transform.rotation;
        cameraRot = cameraPosition.rotation;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        rb.useGravity = false;

        col.enabled = false;

        movementScript.enabled = false;
        lookScript.enabled = false;

        isMoving = true;

        person.transform.position = movePosition.position;
        person.transform.rotation = movePosition.rotation;
        cameraPosition.rotation = Quaternion.Euler(90f, 0f, 0f);
        isMoving = false;
        awaitingEscape = true;
    }


    public void RestoreState()
    {
        stoveIteraction.enabled = false;

        person.transform.position = savedPos;
        person.transform.rotation = savedRot;
        cameraPosition.rotation = cameraRot;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        col.enabled = true;
        movementScript.enabled = true;
        lookScript.enabled = true;

        if (awaitingEscape)
        {
            awaitingEscape = false;
            this.enabled = false;
        }
    }
}
