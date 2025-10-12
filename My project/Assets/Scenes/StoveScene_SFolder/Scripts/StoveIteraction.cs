using UnityEngine;

public class StoveIteraction : MonoBehaviour
{
    public ParticleSystem part1;
    public ParticleSystem part2;
    public ParticleSystem part3;
    public ParticleSystem part4;

    public PancakeTimer pancakeTimer;

    public float soundStartTime;

    private AudioSource switchSound;

    private int offCounter = 0;

    private PlayerMoveToTarget playerMoveToTarget;

    void Start()
    {
        playerMoveToTarget = GetComponent<PlayerMoveToTarget>();
        switchSound = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Stove1"))
                {
                    ParticleSystem.EmissionModule emiss = part1.emission;
                    if (emiss.enabled == true)
                    {
                        emiss.enabled = false;
                        offCounter++;
                        switchSound.Stop();
                        switchSound.time = soundStartTime;
                        switchSound.Play();
                    }
                }

                else if (hit.collider.gameObject.CompareTag("Stove2"))
                {
                    ParticleSystem.EmissionModule emiss = part2.emission;
                    if (emiss.enabled == true)
                    {
                        emiss.enabled = false;
                        offCounter++;
                        switchSound.Stop();
                        switchSound.time = soundStartTime;
                        switchSound.Play();
                    }
                }

                else if (hit.collider.gameObject.CompareTag("Stove3"))
                {
                    ParticleSystem.EmissionModule emiss = part3.emission;
                    if (emiss.enabled == true)
                    {
                        emiss.enabled = false;
                        offCounter++;
                        switchSound.Stop();
                        switchSound.time = soundStartTime;
                        switchSound.Play();
                    }
                }

                else if (hit.collider.gameObject.CompareTag("Stove4"))
                {
                    ParticleSystem.EmissionModule emiss = part4.emission;
                    if (emiss.enabled == true)
                    {
                        emiss.enabled = false;
                        offCounter++;
                        switchSound.Stop();
                        switchSound.time = soundStartTime;
                        switchSound.Play();
                    }
                }
            }

            if (offCounter == 4)
            {
                pancakeTimer.incCounter();
                playerMoveToTarget.RestoreState();
            }
        }
    }

    public void Reset()
    {
        ParticleSystem.EmissionModule emiss = part1.emission;
        emiss.enabled = true;
        emiss = part2.emission;
        emiss.enabled = true;
        emiss = part3.emission;
        emiss.enabled = true;
        emiss = part4.emission;
        emiss.enabled = true;
        offCounter = 0;
    }
}
