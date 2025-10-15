using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    [Header("Audio clips")]
    public AudioClip OST;
    public AudioClip footsteps;
    public AudioClip blinPickup;
    public AudioClip buttonSFX;

    void Start()
    {
        if (MainMenu.mainMenuActive)
        {
            music.clip = OST;
            music.Play();
        }
    }

    // Update is called once per frame
    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
