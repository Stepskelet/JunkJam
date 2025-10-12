using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class PancakeTimer : MonoBehaviour
{
    [Header("Время ожидания (секунды)")]
    public float waitTime = 10f;

    [Header("Image для затемнения экрана")]
    public Image fadeImage;

    [Header("Скорость затемнения")]
    public float fadeSpeed = 2f;

    [Header("Объект блина")]

    public GameObject pancake;

    [Header("Звуки")]

    public AudioSource stoveAudio;
    public AudioSource suffAudio;

    public AudioSource completeSound;

    [Header("Значения для звука конфорок")]

    public float soundStart;

    public float soundEnd;
    

    private bool isFading = false;
    private float fadeAlpha = 0f;

    private int stovesOff = 0;


    void Start()
    {
        pancake.SetActive(false);
        StartCoroutine(WaitAndFade());
        stoveAudio.time = soundStart;
        stoveAudio.Play();
    }

    void Update()
    {
        if (stoveAudio.isPlaying)
        {
            if (stoveAudio.time >= soundEnd)
            {
                stoveAudio.time = soundStart;
            }
        }
    }

    IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(waitTime);

        if (stovesOff != 4)
        {
            isFading = true;
            stoveAudio.Stop();
            suffAudio.Play();
            while (fadeAlpha < 1f)
            {
                fadeAlpha += Time.deltaTime * fadeSpeed;
                fadeImage.color = new Color(0, 0, 0, fadeAlpha);
                yield return null;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void incCounter()
    {
        stovesOff++;

        if (stovesOff == 4 && !isFading)
        {
            pancake.SetActive(true);
            stoveAudio.Stop();
            completeSound.Play();
            this.enabled = false;
        }
    }
}
