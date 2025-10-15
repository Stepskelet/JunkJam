using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public class StoveClick : MonoBehaviour
{
    public float interactDistance = 1f;    // Дистанция взаимодействия

    public PancakeFlip toFlip;
    public LiftAndDrop pan;

    public HintManager hint;

    public BlinCounterScript counter;

    private bool isPlaying = false;

    private bool updated = false;

    private bool isFading = false;

    private AudioSource fryingSound;

    private float fadeAlpha = 0f;

    public Image fadeImage;

    void Start()
    {
        fryingSound = GetComponent<AudioSource>();
    }

    IEnumerator WaitAndFade()
    {

        float fadeSpeed = 0.2f;
        while (fadeAlpha < 1f)
        {
            fadeAlpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
            yield return null;
        }

        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (pan.isDown && !isFading)
        {
            isFading = true;
            fryingSound.Play();
            StartCoroutine(WaitAndFade());
        }

        if (isPlaying && pan.isUp && !toFlip.flipped)
        {
            toFlip.Flip();
        }

        else if (isPlaying && pan.isUp && toFlip.flipped && !pan.isDown)
        {
            if (!updated)
            {
                counter.UpdateCounter(1);
                updated = true;
            }
            pan.PutDown();
        }


        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        if (Input.GetKeyDown(KeyCode.E) && !isPlaying)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    isPlaying = true;
                    hint.HideHint();
                    pan.LiftUp();
                }
            }
        }

        if (!isPlaying)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider == GetComponent<Collider>())
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
        }   
    }
}
